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
            if (companion.safeLocations.Count > 0)
            {
                Vector3 target = companion.safeLocations[companion.safeLocations.Count - 1];
                companion.safeLocations.Remove(target);

                blackboard.SetValue("target", target);
                companion.ChangeAction(CompanionAction.HIDE);
                return NodeState.SUCCESS;
            }
            else
            {
                Vector3 dir = companion.transform.position - companion.eyes.spottedEnemyPosition.transform.position;
                dir.Normalize();

                Vector3 target = companion.transform.position + dir;
                target *= 1.5f;
                blackboard.SetValue("target", target);
                companion.ChangeAction(CompanionAction.HIDE);
                return NodeState.SUCCESS;
            }
        }
        else if (companion.currentAction == CompanionAction.HIDE)
        {
            if (companion.safeLocations.Count > 0)
            {
                Vector3 target = companion.safeLocations[companion.safeLocations.Count - 1];
                companion.safeLocations.Remove(target);

                blackboard.SetValue("target", target);
                companion.ChangeAction(CompanionAction.HIDE);
                return NodeState.SUCCESS;
            }
            else
            {
                Vector3 dir = companion.transform.position - companion.eyes.spottedEnemyPosition.transform.position;
                dir.Normalize();

                Vector3 target = companion.transform.position + dir;
                target *= 1.5f;
                blackboard.SetValue("target", target);
                companion.ChangeAction(CompanionAction.HIDE);
                return NodeState.SUCCESS;
            }
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}