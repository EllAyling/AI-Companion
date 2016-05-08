using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CompanionAction
{
    FOLLOW,
    HIDE,
    SCAVENGE,
    OVERWATCH,
    FLANK,
}

public enum Stance
{
    PASSIVE,
    DEFENSIVE,
    AGGRESSIVE,
}

public enum FollowState
{
    CLOSE,
    FAR
}

public class Companion : Entity
{
    public Player player;

    public CompanionAction currentAction;
    public Stance currentStance;
    public FollowState FollowState;
    public float followDistance;

    Entity enemyLookedAt;
    
    private GunController gunController;
    private AIBrain brain;

    public bool flanking = false;
    public bool hiding = false;
    public List<Vector3> safeLocations;

    public List<Vector3> candidatePositionsForDebug;
    public bool showDebug;

    public override void Start()
    {
        base.Start();
        EntityType type = EntityType.Companion;
        AddType(type);

        safeLocations = new List<Vector3>();
        InvokeRepeating("MarkLocation", 1, 5);

        gunController = GetComponent<GunController>();

        blackboard.treeData.Add("companion", this);
        blackboard.treeData.Add("eyes", eyes);
        blackboard.treeData.Add("gunController", gunController);

        NodeSequencer root = new NodeSequencer(new BTNode[]
            {
                    new NodeSelector(new BTNode[]
                    {
                        new NodeAlwaysFail(
                            new ActionCheckHitLocation()
                        ),
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionReachedTarget(),
                            new NodeInverter(
                                new ActionCheckHideTimer()
                            ),
                            new ActionGetHidePosition(),
                            new ActionRequestPathToTarget()
                        }),
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionCheckOverwatch(),
                            new ActionReachedTarget(),
                            new ActionLookForEnemy(),
                            new NodeCounter(
                                new NodeSequencer(new BTNode[]
                                {
                                    new ActionGetOverwatchPosition(),
                                    new ActionRequestPathToTarget()
                                }), 4
                            )
                        }),
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionCheckForEnemiesInSight(),
                            new NodeSelector(new BTNode[]
                            {
                                new ActionCheckAggressiveStance(),
                                new ActionCheckDefensiveStance(),

                                    new NodeCounter(
                                        new NodeSequencer(new BTNode[]
                                        {
                                            new ActionReachedTarget(),
                                            new ActionGetHidePosition(),
                                            new ActionRequestPathToTarget()
                                        }), 1
                                    )
                            
                            })
                        }),
                        new ActionToggleFlank(),
                        new NodeInverter(
                            new NodeSelector(new BTNode[]
                            {
                                new NodeInverter(
                                    new ActionCheckScavangeAction()
                                    ),
                                new NodeInverter(
                                    new NodeSelector(new BTNode[]
                                    {
                                        new NodeCounter(
                                            new ActionCheckScavangeStatus(),10
                                            ),
                                        new NodeCounter(
                                            new NodeSequencer(new BTNode[]
                                            {
                                                new ActionReachedTarget(),
                                                new ActionGetScavangeLocation(),
                                                new ActionRequestPathToTarget()
                                            }), 5
                                            )
                                    })
                                ),
                                new NodeSequencer(new BTNode[]
                                {
                                    new ActionStopMovement(),
                                    new ActionGiveMedKitToPlayer(),
                                    new ActionRequestPathToTarget()
                                })
                            })
                        ),
                        new NodeSequencer(new BTNode[]
                        {
                            new NodeCounter(
                                new NodeInverter(
                                    new ActionCheckFollowPosition()
                                ), 0.5f
                            ),
                            new NodeSequencer(new BTNode[]
                            {
                                new ActionReachedTarget(),
                                new ActionGetFollowPosition(),
                                new ActionRequestPathToTarget()
                            })
                        })
                }),
                new NodeSelector(new BTNode[]
                {
                    new NodeSequencer(new BTNode[]
                    {
                        new NodeCounter(
                            new NodeInverter(
                                new ActionCheckFollowPosition()
                                ), 2
                        ),
                        new ActionGetFollowPosition(),
                        new ActionRequestPathToTarget()
                    }),
                    new NodeSelector(new BTNode[]
                    {
                        new NodeSequencer(new BTNode[]
                        {
                            new NodeInverter(
                                new ActionCheckDefensiveStance()
                            ),
                            new ActionCheckForEnemiesInSight(),
                            new NodeAlwaysFail(
                                new ActionUseWeapon()
                            )
                        }),
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionCheckPlayerTarget(),
                            new ActionUseWeapon()
                        })
                    }),
                    new NodeSelector(new BTNode[]
                    {
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionCheckFlankAction(),
                            new NodeSequencer(new BTNode[]
                            {
                                new ActionGetAttackPosition(),
                                new ActionRequestPathToTarget()
                            })
                        }),
                        new NodeSequencer(new BTNode[]
                        {
                            new NodeInverter(
                                new ActionCheckFlankAction()
                            ),
                            new ActionReachedTarget(),
                            new ActionToggleFlank()
                        })
                    })
                })
            });

        brain = new AIBrain(root, blackboard);
        brain.Start();
    }

    // Update is called once per frame
    void Update()
    {
        brain.Update();

        if (player.enemyLastFiredAt && enemyLookedAt != player.enemyLastFiredAt)
        {
            transform.LookAt(player.enemyLastFiredAt.transform);
            enemyLookedAt = player.enemyLastFiredAt;
        }


    }

    public void ChangeStance(Stance newStance)
    {
        currentStance = newStance;
    }

    public void ChangeAction(CompanionAction newAction)
    {
        currentAction = newAction;
    }

    public void ChangeFollowState(FollowState newState)
    {
        FollowState = newState;
    }

    public void MarkLocation()
    {
        if (currentAction != CompanionAction.HIDE)
        {
            if (!safeLocations.Contains(transform.position))
            {
                safeLocations.Add(transform.position);
            }
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (showDebug)
        {
            if (candidatePositionsForDebug.Count > 0)
            {
                Gizmos.color = Color.blue;
                foreach (Vector3 position in candidatePositionsForDebug)
                {
                    Gizmos.DrawCube(position, Vector3.one);
                    Gizmos.DrawLine(position, player.transform.position);
                }
            }

            if (safeLocations.Count > 0)
            {
                foreach (Vector3 position in safeLocations)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(position, Vector3.one);
                }
            }
        }
    }
}
