using UnityEngine;
using System.Collections;

public class ActionGetLastSightedSearchPosition : BTNode {

    private Entity entity;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");

    }

    public override NodeState Tick()
    {
        if (entity.getSearchPosition)
        {
            Transform target = blackboard.GetValueFromKey<Transform>("spottedPlayerPosition");
            blackboard.SetValue<Vector3>("target", target.position);
            entity.getSearchPosition = false;
            entity.searchingForEnemy = true;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
