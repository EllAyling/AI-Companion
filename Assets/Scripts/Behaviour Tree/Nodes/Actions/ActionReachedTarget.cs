using UnityEngine;
using System.Collections;

public class ActionReachedTarget : BTNode {

    Entity entity;
    Transform target;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
        target = blackboard.GetValueFromKey<Transform>("target");

        if (target == null)
        {
            return NodeState.SUCCESS;
        }

        if (Vector3.Distance(entity.transform.position, target.position) < 2.0f)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
