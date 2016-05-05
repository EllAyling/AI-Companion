using UnityEngine;
using System.Collections;

public class ActionCheckHideTimer : BTNode
{

    private Companion companion;
    private float comeFromHideTick;
    private float tickTime;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
        comeFromHideTick = 7;
        tickTime = 0;

    }

    public override NodeState Tick()
    {
        if (companion.currentAction == CompanionAction.HIDE)
        {
            if (tickTime < comeFromHideTick)
            {
                tickTime += Time.deltaTime;
                return NodeState.SUCCESS;
            }
            else
            {
                tickTime = 0;
                return NodeState.FAILURE;
            }
        }
        else
        {
            tickTime = comeFromHideTick;
            return NodeState.SUCCESS;
        }
    }
}
