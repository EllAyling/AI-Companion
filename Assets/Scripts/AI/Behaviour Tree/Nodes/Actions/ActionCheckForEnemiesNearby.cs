using UnityEngine;
using System.Collections;

public class ActionCheckForEnemiesNearby : BTNode {

    private Entity entity;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");      
    }

    public override NodeState Tick()
    {
        if (entity.enemyNearby)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
