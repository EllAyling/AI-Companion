using UnityEngine;
using System.Collections;

public class ActionCheckFollowPosition : BTNode
{
    private Companion companion;
    private AISight eyes;
    private float distanceFromPlayer;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
        eyes = companion.GetComponentInChildren<AISight>();
    }

    public override NodeState Tick()
    {
        if (companion.FollowState == FollowState.CLOSE)
        {
            distanceFromPlayer = 4.0f;
        }
        else
        {
            distanceFromPlayer = 20.0f;
            if (eyes.enemiesInSight.Count > 0)
            {
                distanceFromPlayer = 8.0f;
            }
        }

        float distance = Vector3.Distance(companion.transform.position, companion.player.transform.position);
        if (distance > distanceFromPlayer && companion.currentAction == CompanionAction.FOLLOW)
        {
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
