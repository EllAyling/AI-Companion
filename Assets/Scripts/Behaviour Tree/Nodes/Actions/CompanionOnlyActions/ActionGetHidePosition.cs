using UnityEngine;
using System.Collections;

public class ActionGetHidePosition : BTNode {

    private Companion companion;
    private Vector3 positionHidingFrom = Vector3.zero;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
        
    }

    public override NodeState Tick()
    {
        if (positionHidingFrom != Vector3.zero)
        {
            if (CheckSightToHidingFromPos())
            {
                Debug.Log("Still see ya");
                Vector3 dir = companion.transform.position - positionHidingFrom;
                dir.Normalize();
                Vector3 target = companion.transform.position + dir;
                target *= 2.0f;
                blackboard.SetValue("target", target);
                return NodeState.SUCCESS;
            }
        }

        if (companion.eyes.enemiesInSight.Count == 1)
        {
            Vector3 dir = companion.transform.position - companion.eyes.enemiesInSight[0].transform.position;
            dir.Normalize();
            positionHidingFrom = companion.eyes.enemiesInSight[0].transform.position;

            Vector3 target = companion.transform.position + dir;
            target *= 3.0f;
            blackboard.SetValue("target", target);
            companion.ChangeAction(CompanionAction.HIDE);
            return NodeState.SUCCESS;
        }
        else if (companion.eyes.enemiesInSight.Count > 1)
        {
            return NodeState.FAILURE;
        }
        else
            return NodeState.FAILURE;
    }

    public bool CheckSightToHidingFromPos()
    {
        if (!Physics.Linecast(companion.transform.position, positionHidingFrom))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
