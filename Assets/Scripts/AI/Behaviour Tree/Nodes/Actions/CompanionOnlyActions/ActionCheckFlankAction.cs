using UnityEngine;
using System.Collections;

public class ActionCheckFlankAction : BTNode
{

    private Companion companion;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
    }

    public override NodeState Tick()
    {
        if (companion.currentAction == CompanionAction.FLANK && !companion.flanking)
        {
            companion.flanking = true;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
