using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CompanionAction
{
    NONE,
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

public class Companion : Entity
{
    public Player player;

    public CompanionAction currentAction;
    public Stance currentStance;

    public AISight eyes;
    private GunController gunController;
    private AIBrain brain;

    public override void Start()
    {
        base.Start();
        EntityType type = EntityType.Companion;
        AddType(type);

        eyes = GetComponentInChildren<AISight>();

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
                            new ActionGetFollowPosition(),
                            new ActionRequestPathToTarget()
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
    }

    public void ChangeStance(Stance newStance)
    {
        currentStance = newStance;
    }

    public void ChangeAction(CompanionAction newAction)
    {
        currentAction = newAction;
    }
}
