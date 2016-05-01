using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionGetNextWaypoint : BTNode {

    private Transform[] waypoints;
    private int nextWaypointIndex;
    private Transform nextWaypoint;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        waypoints = blackboard.GetValueFromKey<Transform[]>("patrolRoute");
    }

    public override NodeState Tick()
    {
        nextWaypointIndex = (nextWaypointIndex + 1) % waypoints.Length;
        nextWaypoint = waypoints[nextWaypointIndex];

        blackboard.SetValue<Transform>("target", nextWaypoint);

        return NodeState.SUCCESS;
    }
}
