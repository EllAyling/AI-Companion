using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        bool first = true;
        float distance = float.MaxValue;
        Vector3 closestPosition = Vector3.zero;
        foreach (KeyValuePair<string, Vector3> medKit in companion.seenMedKits)
        {
            if (first)
            {
                closestPosition = medKit.Value;
                distance = Vector3.Distance(companion.transform.position, closestPosition);
                first = false;
                continue;
            }

            Vector3 position = medKit.Value;
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
