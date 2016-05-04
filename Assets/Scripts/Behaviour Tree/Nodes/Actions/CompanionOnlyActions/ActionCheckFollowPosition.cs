using UnityEngine;
using System.Collections;

public class ActionCheckFollowPosition : BTNode
{

    private Companion companion;
    private float comeFromHideTick;
    private float tickTime;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
        comeFromHideTick = 5;
        tickTime = 0;
    }

    public override NodeState Tick()
    {
        float distance = Vector3.Distance(companion.transform.position, companion.player.transform.position);
        if (distance > 5.0f && companion.currentAction == CompanionAction.NONE)
        {
            return NodeState.FAILURE;
        }
        else if (companion.currentAction == CompanionAction.HIDE)
        {
            if (tickTime < comeFromHideTick)
            {
                tickTime += Time.deltaTime;
                return NodeState.SUCCESS;
            }
            else
            {
                companion.ChangeAction(CompanionAction.NONE);
                tickTime = 0;
                return NodeState.FAILURE;
            }
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
