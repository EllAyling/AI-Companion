using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionGetNextWaypoint : BTNode {

    private Transform[] waypoints;
    private int nextWaypointIndex;
    private Vector3 nextWaypoint;
    private Entity entity;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        waypoints = blackboard.GetValueFromKey<Transform[]>("patrolRoute");
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
        nextWaypointIndex = (nextWaypointIndex + 1) % waypoints.Length;
        nextWaypoint = waypoints[nextWaypointIndex].position;
        entity.transform.LookAt(waypoints[nextWaypointIndex]);

        blackboard.SetValue("target", nextWaypoint);
        return NodeState.SUCCESS;
    }
}
