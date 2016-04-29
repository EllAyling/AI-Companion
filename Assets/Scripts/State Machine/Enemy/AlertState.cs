using UnityEngine;
using System.Collections;

public class AlertState : IEnemyState {

    private readonly StatePatternEnemy enemy;

    private float searchTime;
    bool cameFromChase = false;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToPatrolState()
    {
        searchTime = 0;
        enemy.currentState = enemy.patrolState;

        if (!cameFromChase) enemy.currentState.FromAlertState();
        else enemy.currentState.FromChaseState();

        Debug.Log("Came from chase " + cameFromChase);

        cameFromChase = false;
    }

    public void FromPatrolState()
    {
        enemy.StopPath();
    }

    public void ToAlertState()
    {
        Debug.Log("Can't transition to same state");
    }
    public void FromAlertState()
    {
        Debug.Log("Can't transition from same state");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        enemy.currentState.FromAlertState();
        searchTime = 0;
    }

    public void FromChaseState()
    {
        enemy.StopPath();
        cameFromChase = true;
    }

    private void Look()
    {
        if (enemy.eyes.playerInSight)
        {
            ToChaseState();
        }
    }

    private void Search()
    {
        enemy.meshRendererFlag.material.color = Color.yellow;
        enemy.transform.Rotate(0, enemy.seachingTurnSpeed * Time.deltaTime, 0);
        searchTime += Time.deltaTime;

        if (searchTime >= enemy.searchingDuration)
            ToPatrolState();
    }
}
