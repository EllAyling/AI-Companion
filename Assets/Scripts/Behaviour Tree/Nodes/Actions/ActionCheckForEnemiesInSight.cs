using UnityEngine;
using System.Collections;

public class ActionCheckForEnemiesInSight : BTNode {

    private EnemySight eyes;
    private Entity entity;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        eyes = blackboard.GetValueFromKey<EnemySight>("eyes");
        entity = eyes.transform.parent.GetComponent<Entity>();
    }

    public override NodeState Tick()
    {
        if (eyes.enemiesInSight.Count > 0)
        {
            blackboard.SetValue("spottedPlayerPosition", eyes.spottedEnemyPosition);
            entity.transform.LookAt(eyes.spottedEnemyPosition.position);
            entity.getSearchPosition = true;
            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}
