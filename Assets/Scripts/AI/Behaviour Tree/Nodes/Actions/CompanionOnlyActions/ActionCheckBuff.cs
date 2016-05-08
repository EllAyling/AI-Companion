using UnityEngine;
using System.Collections;

public class ActionCheckBuff : BTNode
{
    private CompanionBuff companion;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<CompanionBuff>("companion");
    }

    public override NodeState Tick()
    {
        if (companion.player.buffed)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
