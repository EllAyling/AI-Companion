using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGuard : Entity {

    private AIBrain brain;
    private GunController gunController;

    // Use this for initialization
    public override void Start () {

        base.Start();
        EntityType type = EntityType.Enemy | EntityType.PatrolEnemy;
        AddType(type);

        speed = 2.0f;

        enemyNearby = false;
        gunController = GetComponent<GunController>();

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
