using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState {

    private readonly StatePatternEnemy stateMachine;
    private readonly EnemySM enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        stateMachine = statePatternEnemy;
        enemy = statePatternEnemy.enemy;
    }

    public void UpdateState()
    {
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToPatrolState()
    {

    }

    public void FromPatrolState()
    {

    }

    public void ToAlertState()
    {
        stateMachine.currentState = stateMachine.alertState;
        stateMachine.currentState.FromChaseState();
    }

    public void FromAlertState()
    {

    }

    public void ToChaseState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void FromChaseState()
    {
        Debug.Log("Can't transition from same state");
    }

    private void Look()
    {
        if (enemy.eyes.playerInSight)
        {
            Chase();
        }
        else
        {
            ToAlertState();
        }
    }

    private void Chase()
    {
        enemy.meshRendererFlag.material.color = Color.red;
        if (enemy.chaseTarget != enemy.eyes.spottedPlayerPosition)
        {
            if (Vector3.Distance(enemy.chaseTarget, enemy.eyes.spottedPlayerPosition) > 2.0f)       //If the player moves too far from the last sighted spot. So we dont request loads of paths per frame.
            {
                enemy.chaseTarget = enemy.eyes.spottedPlayerPosition;
                enemy.RequestNewPath(enemy.chaseTarget);
            }
                enemy.transform.LookAt(enemy.chaseTarget);
        }
    }
}
