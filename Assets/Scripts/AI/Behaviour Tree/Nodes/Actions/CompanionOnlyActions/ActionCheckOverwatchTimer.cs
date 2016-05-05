using UnityEngine;
using System.Collections;

public class ActionCheckOverwatchTimer : BTNode
{

    private Companion companion;

    private float comeFromOverwatchTick;
    private float tickTime;


    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
        comeFromOverwatchTick = 20;
        tickTime = comeFromOverwatchTick;
    }

    public override NodeState Tick()
    {
        if (companion.currentAction == CompanionAction.OVERWATCH)
        {
            if (tickTime < comeFromOverwatchTick)
            {
                tickTime += Time.deltaTime;
                return NodeState.FAILURE;
            }
            else
            {
                tickTime = 0;
                return NodeState.SUCCESS;
            }
        }
        else
        {
            tickTime = comeFromOverwatchTick;
            return NodeState.SUCCESS;
        }
    }
}
