using UnityEngine;
using System.Collections;

public class ActionCheckPlayerTarget : BTNode {

    Companion companion;
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("entity");
    }

    public override NodeState Tick()
    {
        Entity enemy = companion.player.enemyLastFiredAt;
        if (enemy)
        {
            companion.transform.LookAt(enemy.transform);
            companion.player.enemyLastFiredAt = null;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
