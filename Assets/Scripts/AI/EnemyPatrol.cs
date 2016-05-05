using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPatrol : Entity {


    public Transform[] patrolRoute;

    private AIBrain brain;
    private GunController gunController;

    // Use this for initialization
    public override void Start () {

        base.Start();

        EntityType type = EntityType.Enemy | EntityType.PatrolEnemy;
        AddType(type);

        enemyNearby = false;
        blackboard = new Blackboard();
        gunController = GetComponent<GunController>();

        blackboard.treeData.Add("patrolRoute", patrolRoute);
        blackboard.treeData.Add("entity", this);
        blackboard.treeData.Add("eyes", eyes);
        blackboard.treeData.Add("gunController", gunController);

        NodeSequencer root = new NodeSequencer(new BTNode[]
            {
                new NodeSelector(new BTNode[]
                {
                    new NodeSelector(new BTNode[]
                    {
                        new ActionCheckForEnemiesInSight(),
                        new NodeAlwaysFail(
                            new NodeSequencer(new BTNode[]
                            {
                                new ActionCheckHitLocation(),
                                new ActionRequestPathToTarget()
                            })
                            ),
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionGetLastSightedSearchPosition(),
                            new ActionRequestPathToTarget()
                        }),
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionSearchingForEnemyBool(),
                            new NodeSelector(new BTNode[]
                            {
                                new NodeInverter(
                                    new ActionReachedTarget()
                                    ),
                                new NodeAlwaysFail(
                                    new NodeSequencer(new BTNode[]
                                    {
                                        new ActionFindNearestWaypoint(),
                                        new ActionRequestPathToTarget()
                                    })
                                )
                            })
                        }),
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionCheckForEnemiesNearby(),
                            new ActionStopMovement()
                        })
                    }),
                    new NodeSequencer(new BTNode[]
                    {
                        new ActionReachedTarget(),
                        new ActionGetNextWaypoint(),
                        new NodeAlwaysFail(
                            new ActionRequestPathToTarget()
                            )
                    })
                }),
                new NodeSelector(new BTNode[]
                {
                    new ActionCheckForEnemiesInSight(),
                    new NodeAlwaysFail(
                        new ActionLookForEnemy()
                    )

                }),
                new NodeSelector(new BTNode[]
                {
                    new NodeSequencer(new BTNode[]
                    {
                        new NodeSequencer(new BTNode[]
                        {
                            new NodeAlwaysSuccess(
                                new NodeCounter(
                                    new ActionGetAttackPosition(), 5)
                                ),
                            new ActionUseWeapon(),
                            new ActionReachedTarget()
                        }),
                            new NodeAlwaysSuccess(
                                new NodeSequencer(new BTNode[]
                                {
                                    new NodeInverter(
                                        new ActionAttackPosition()
                                    ),
                                    new ActionGetAttackPosition(),
                                    new ActionRequestPathToTarget()
                                })),
                    }),
                    new ActionRequestPathToTarget()
                })
            });

        brain = new AIBrain(root, blackboard);
        brain.Start();
    }

    void Update()
    {
        brain.Update();
    }
}
