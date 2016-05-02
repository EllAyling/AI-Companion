using UnityEngine;
using System.Collections;

public class ActionReachedTarget : BTNode {

    Entity entity;
    Vector3 target;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
        target = Vector3.zero;
    }

    public override NodeState Tick()
    {
        target = blackboard.GetValueFromKey<Vector3>("target");

        if (target == Vector3.zero)
        {
            return NodeState.SUCCESS;
        }

        if (Vector3.Distance(entity.transform.position, target) < 2.0f)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
