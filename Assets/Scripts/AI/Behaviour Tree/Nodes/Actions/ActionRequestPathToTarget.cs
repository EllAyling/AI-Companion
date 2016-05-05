using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionRequestPathToTarget : BTNode {

    private Entity entity;
    private Vector3 target;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
        target = blackboard.GetValueFromKey<Vector3>("target");
        entity.RequestNewPath(target);
        return NodeState.SUCCESS;
    }
}
