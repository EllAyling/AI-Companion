using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;
    private bool wayPointReached;
    private bool findNearestWaypoint;

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;       
    }

    public void UpdateState()
    {
        Look();
        Patrol();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.StopPath();
            ToAlertState();
        }
    }

    public void ToPatrolState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void FromPatrolState()
    {
        Debug.Log("Can't transition from same state");
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
        enemy.currentState.FromPatrolState();
    }

    public void FromAlertState()
    {
        enemy.RequestNewPath(enemy.wayPoints[nextWayPoint].position);
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        enemy.currentState.FromPatrolState();
    }

    public void FromChaseState()
    {
        GoToNearestWaypoint();
    }

    private void Look()
    {
        if (enemy.eyes.playerInSight)
        {
            ToChaseState();
        }
    }

    private void Patrol()
    {
        enemy.meshRendererFlag.material.color = Color.green;

        if (Vector3.Distance(enemy.transform.position, enemy.wayPoints[nextWayPoint].position) < 2.0f)
            wayPointReached = true;

        if (wayPointReached)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
            enemy.RequestNewPath(enemy.wayPoints[nextWayPoint].position);
            wayPointReached = false;
        }
    }

    private void GoToNearestWaypoint()
    {
        nextWayPoint = 0;
        for (int i = 0; i < enemy.wayPoints.Length; i++)
        {
            if (i == nextWayPoint) continue;

            if (Vector3.Distance(enemy.wayPoints[i].position, enemy.transform.position) < Vector3.Distance(enemy.wayPoints[nextWayPoint].position, enemy.transform.position))
            {
                nextWayPoint = i;
            }
        }
        enemy.RequestNewPath(enemy.wayPoints[nextWayPoint].position);
    } 
}
