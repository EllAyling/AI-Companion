using UnityEngine;
using System.Collections;

public class AlertState : IEnemyState {

    private readonly StatePatternEnemy stateMachine;
    private readonly Enemy enemy;

    private float searchTime;
    bool cameFromChase = false;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        stateMachine = statePatternEnemy;
        enemy = statePatternEnemy.enemy;
        
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
        stateMachine.currentState = stateMachine.patrolState;

        if (!cameFromChase) stateMachine.currentState.FromAlertState();
        else stateMachine.currentState.FromChaseState();

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
        stateMachine.currentState = stateMachine.chaseState;
        stateMachine.currentState.FromAlertState();
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
