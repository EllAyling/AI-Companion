using UnityEngine;
using System.Collections;

public class EnemyHunter : Entity {

    private AIBrain brain;
    private GunController gunController;

    // Use this for initialization
    public override void Start()
    {

        base.Start();

        EntityType type = EntityType.Enemy | EntityType.HunterEnemy;
        AddType(type);

        enemyNearby = false;
        blackboard = new Blackboard();
        gunController = GetComponent<GunController>();

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
                            new NodeInverter(
                                new ActionReachedTarget()
                            )
                        }),
                        new ActionCheckForEnemiesNearby()
                    }),
                    new NodeSequencer(new BTNode[]
                    {
                        new ActionReachedTarget(),
                        new ActionLookForEnemy(),
                        new NodeCounter(
                            new ActionMoveToPlayer(), 5
                        ),
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
                                    new ActionMoveToPlayer(), 1
                                )
                            ),
                            new ActionUseWeapon(),
                            new ActionReachedTarget()
                        })
                    }),
                    new NodeAlwaysSuccess(
                        new NodeSequencer(new BTNode[]
                        {
                            new NodeInverter(
                                new ActionCheckAttackPosition()
                             ),
                            new ActionMoveToPlayer(),
                            new ActionRequestPathToTarget()
                        })
                   )
                }),
                new ActionRequestPathToTarget()
            });

        brain = new AIBrain(root, blackboard);
        brain.Start();
    }

    void Update()
    {
        brain.Update();
    }
}
