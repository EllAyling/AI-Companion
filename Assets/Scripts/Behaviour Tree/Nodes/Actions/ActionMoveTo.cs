using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionMoveTo : BTNode {

    private Entity entity;
    private Transform target;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
        target = blackboard.GetValueFromKey<Transform>("target");
        entity.RequestNewPath(target);
        return NodeState.SUCCESS;
    }
}
