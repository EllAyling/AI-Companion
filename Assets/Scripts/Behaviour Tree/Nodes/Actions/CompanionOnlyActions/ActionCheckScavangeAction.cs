using UnityEngine;
using System.Collections;

public class ActionCheckScavangeAction : BTNode {

    Companion companion;

    // Use this for initialization
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("entity");
    }

    public override NodeState Tick()
    {
        if (companion.currentAction == CompanionAction.SCAVENGE)
        {
            Debug.Log(companion.currentAction);
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
