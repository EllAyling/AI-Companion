using UnityEngine;
using System.Collections;

public class ActionFindNearestWaypoint : BTNode {

    private Transform[] waypoints;
    private Entity entity;
    private int nextWaypointIndex;
    private Transform nextWaypoint;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        waypoints = blackboard.GetValueFromKey<Transform[]>("patrolRoute");
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
        nextWaypointIndex = 0;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (i == nextWaypointIndex) continue;

            if (Vector3.Distance(waypoints[i].position, entity.transform.position) < Vector3.Distance(waypoints[nextWaypointIndex].position, entity.transform.position))
            {
                nextWaypointIndex = i;
            }
        }

        blackboard.SetValue<Transform>("target", waypoints[nextWaypointIndex]);

        return NodeState.SUCCESS;
    }
}
