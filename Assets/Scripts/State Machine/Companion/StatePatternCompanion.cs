using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatePatternCompanion : MonoBehaviour {

    [HideInInspector] public ICompanionState currentState;

    [HideInInspector] public FollowState followState;
    [HideInInspector] public CombatState combatState;

    [HideInInspector] public CompanionSM companion;

    // Use this for initialization
    void Awake ()
    {
        companion = GetComponent<CompanionSM>();
        followState = new FollowState(this);
        combatState = new CombatState(this);
    }

    void Start()
    {
        currentState = followState;
        currentState.FromCombatState();
    }

	void Update ()
    {
        currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
