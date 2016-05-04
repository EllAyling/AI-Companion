using UnityEngine;
using System.Collections;

public class ActionGetScavangeLocation : BTNode
{
    Companion companion;

    // Use this for initialization
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("entity");
    }

    public override NodeState Tick()
    {
        Vector3 target;
        if (companion.seenMedKits.Count > 0)
        {
            target = GetNearestSeenMedkitPosition();
            blackboard.SetValue<Vector3>("target", target);
            companion.transform.LookAt(target);
            return NodeState.SUCCESS;
        }
        else
        {
            target = Search();
            blackboard.SetValue<Vector3>("target", target);
            companion.transform.LookAt(target);
            return NodeState.SUCCESS;
        }
    }

    public Vector3 GetNearestSeenMedkitPosition()
    {

        Vector3 closestPosition = companion.seenMedKits[0];
        float distance = Vector3.Distance(companion.transform.position, companion.seenMedKits[0]);
        for (int i = 1; i < companion.seenMedKits.Count; i++)
        {
            Vector3 position = companion.seenMedKits[i];
            float distanceNew = Vector3.Distance(companion.transform.position, position);
            if (distanceNew < distance)
            {
                closestPosition = position;
            }
        }

        return closestPosition;
    }

    public Vector3 Search()
    {
        Vector2 target = Random.insideUnitCircle * 5;

        Vector3 finalTarget = companion.transform.position + new Vector3(target.x, 0.0f, target.y); //Change from xy plane to xz plane
        finalTarget *= 1.4f;

        if (Vector3.Distance(finalTarget, companion.player.transform.position) > 30.0f)
        {
            finalTarget = companion.player.transform.position + new Vector3(target.x, 0.0f, target.y);
            target *= 4.0f;
        }

        return finalTarget;
    }
}
