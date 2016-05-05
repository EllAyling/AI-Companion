using UnityEngine;
using System.Collections;

public class ActionCheckOverwatch : BTNode
{

    private Companion companion;
   

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
    }

    public override NodeState Tick()
    {
        if (companion.currentAction == CompanionAction.OVERWATCH)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
