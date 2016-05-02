using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPatrol : Entity {


    public Transform[] patrolRoute;

    private AIBrain brain;
    private EnemySight eyes;
    private Blackboard blackboard;
    private GunController gunController;

    // Use this for initialization
    public override void Start () {

        base.Start();
        EntityType type = EntityType.Enemy | EntityType.PatrolEnemy;
        AddType(type);

        speed = 2.0f;

        eyes = GetComponentInChildren<EnemySight>();
        enemyNearby = false;
        blackboard = new Blackboard();
        gunController = GetComponent<GunController>();

        NodeSequencer root = new NodeSequencer(new BTNode[]
            {
                new NodeSelector(new BTNode[]
                {
                        new NodeSelector(new BTNode[]
                        {
                            new ActionCheckForEnemiesInSight(),
                            new NodeSequencer(new BTNode[]
                            {
                                new ActionCheckForEnemiesNearby(),
                                new ActionStopMovement()
                            })
                        }),
                    new NodeSequencer(new BTNode[]
                    {
                        new NodeSequencer(new BTNode[]
                        {
                            new ActionReachedTarget(),
                            new ActionGetNextWaypoint()
                        }),
                        new NodeAlwaysFail(
                            new ActionRequestPathToTarget()
                        )
                    })
                }),
                new NodeSelector(new BTNode[]
                {
                    new ActionCheckForEnemiesInSight(),
                    new NodeAlwaysFail(
                        new ActionLookForHeardEnemy()
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
                        new NodeSequencer(new BTNode[]
                        {
                            new NodeAlwaysSuccess(
                                new NodeSequencer(new BTNode[]
                                {
                                    new NodeInverter(
                                        new ActionCheckPosition()
                                    ),
                                    new ActionGetAttackPosition(),
                                    new ActionRequestPathToTarget()
                                })),
                        })
                    }),
                    new ActionRequestPathToTarget()
                })
            });
        
            

        blackboard.treeData.Add("patrolRoute", patrolRoute);
        blackboard.treeData.Add("entity", this);
        blackboard.treeData.Add("eyes", eyes);
        blackboard.treeData.Add("gunController", gunController);

        brain = new AIBrain(root, blackboard);

        brain.Start();
    }

    void Update()
    {
        brain.Update();
    }
}
