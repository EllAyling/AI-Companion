using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGuard : Entity {

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

        blackboard.treeData.Add("entity", this);
        blackboard.treeData.Add("eyes", eyes);
        blackboard.treeData.Add("gunController", gunController);

        NodeSequencer root = new NodeSequencer(new BTNode[]
            {
                new NodeSelector(new BTNode[]
                {
                    new ActionCheckForEnemiesInSight(),
                        new NodeAlwaysFail(
                            new ActionLookForEnemy()
                        )
                }),
                new ActionUseWeapon()
           });
        

        brain = new AIBrain(root, blackboard);

        brain.Start();
    }

    void Update()
    {
        brain.Update();
    }
}
