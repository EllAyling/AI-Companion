using UnityEngine;
using System.Collections;

public class ActionToggleBuff : BTNode
{
    private CompanionBuff companion;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<CompanionBuff>("companion");
    }

    public override NodeState Tick()
    {
        companion.player.buffed = true;
        return NodeState.SUCCESS;
    }
}
