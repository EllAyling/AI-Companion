using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatState : ICompanionState {

    private readonly CompanionSM companion;
    private readonly StatePatternCompanion stateMachine;

    //float coolDownTimer = 0.0f;
    //float coolDownThreshold = 5.0f;


    public CombatState(StatePatternCompanion statePatternCompanion)
    {
        companion = statePatternCompanion.companion;
        stateMachine = statePatternCompanion;
    }

    public void UpdateState()
    {
        CheckPosition();
        CheckCoolDown();
    }

    public void OnTriggerEnter(Collider other)
    {
    
    }

    public void ToFollowState()
    {
        stateMachine.currentState = stateMachine.followState;
        stateMachine.currentState.FromCombatState();
    }

    public void FromFollowState()
    {
        companion.speed = 8;
        companion.maximumDistance = 100.0f;
        companion.playerRadiusX = 2.0f;
        companion.playerRadiusY = 2.0f;

    }

    public void ToCombatState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void FromCombatState()
    {
        Debug.Log("Can't transition from same state");
    }

    void CheckPosition()
    {
        companion.GetCandidatePositions();
        if (AStar.GetDistance(companion.grid.NodeFromWorldPoint(companion.transform.position), companion.grid.NodeFromWorldPoint(companion.player.transform.position)) > companion.maximumDistance)
        {
            int x = Random.Range(0, companion.candidatePositions.Count - 1);
            Vector3 targetPos = companion.candidatePositions[x];
            companion.RequestNewPath(targetPos);
        }
    }

    void CheckCoolDown()
    {
        /*
        if (companion.player.inCombat)
        {
            coolDownTimer = 0.0f;
            return;
        }

        coolDownTimer += Time.deltaTime;

        if (coolDownTimer > coolDownThreshold)
        {
            coolDownTimer = 0.0f;
            ToFollowState();
        }
        */
    }

}
