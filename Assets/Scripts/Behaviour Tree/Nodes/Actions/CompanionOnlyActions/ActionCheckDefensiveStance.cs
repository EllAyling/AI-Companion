using UnityEngine;
using System.Collections;

public class ActionCheckDefensiveStance : BTNode {

    Companion companion;

    // Use this for initialization
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("entity");
    }

    public override NodeState Tick()
    {
        if (companion.currentStance == Stance.DEFENSIVE)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
