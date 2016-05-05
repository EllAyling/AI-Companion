using UnityEngine;
using System.Collections;

public class ActionGetHidePosition : BTNode
{

    private Companion companion;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
    }

    public override NodeState Tick()
    {
        if (companion.eyes.enemiesInSight.Count > 0)
        {
            Vector3 dir = companion.transform.position - companion.eyes.spottedEnemyPosition.transform.position;
            dir.Normalize();

            Vector3 target = companion.transform.position + dir;
            target *= 1.5f;
            blackboard.SetValue("target", target);
            companion.ChangeAction(CompanionAction.HIDE);
            return NodeState.SUCCESS;
        }
        else if (companion.currentAction == CompanionAction.HIDE)
        {
            Vector3 dir = companion.transform.position - companion.player.transform.position;
            dir.Normalize();

            Vector3 target = companion.transform.position + dir;
            target *= 1.5f;
            blackboard.SetValue("target", target);
            companion.ChangeAction(CompanionAction.FOLLOW);
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}