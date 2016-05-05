using UnityEngine;
using System.Collections;

public class ActionCheckScavangeStatus : BTNode
{

    Companion companion;

    // Use this for initialization
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("entity");
    }

    public override NodeState Tick()
    {
        if (companion.medKits > 0)
        {
            companion.currentAction = CompanionAction.FOLLOW;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
