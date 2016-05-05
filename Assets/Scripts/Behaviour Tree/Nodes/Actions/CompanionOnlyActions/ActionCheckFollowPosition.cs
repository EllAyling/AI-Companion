using UnityEngine;
using System.Collections;

public class ActionCheckFollowPosition : BTNode
{

    private Companion companion;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
    }

    public override NodeState Tick()
    {
        float distance = Vector3.Distance(companion.transform.position, companion.player.transform.position);
        if (distance > 5.0f && companion.currentAction == CompanionAction.NONE)
        {
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
