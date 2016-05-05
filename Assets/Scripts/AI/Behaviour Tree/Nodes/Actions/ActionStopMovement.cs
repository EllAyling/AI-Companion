using UnityEngine;
using System.Collections;
using System;

public class ActionStopMovement : BTNode {

    private Entity entity;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
        entity.StopPath();
        blackboard.SetValue("target", Vector3.zero);
        Debug.Log("Stop movement");
        return NodeState.SUCCESS;
    }

}
