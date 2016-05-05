using UnityEngine;
using System.Collections;

public class ActionToggleFlank : BTNode {

    Companion companion;

    // Use this for initialization
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("entity");
    }

    public override NodeState Tick()
    {
        if (companion.currentAction == CompanionAction.FLANK)
        {
            companion.currentAction = CompanionAction.FOLLOW;
            companion.flanking = false;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
