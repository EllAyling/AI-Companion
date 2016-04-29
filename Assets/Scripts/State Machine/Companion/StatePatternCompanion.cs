using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatePatternCompanion : Companion {

    [HideInInspector] public ICompanionState currentState;

    [HideInInspector] public FollowState followState;
    [HideInInspector] public CombatState combatState;

    [HideInInspector] public Companion companion;

    // Use this for initialization
    void Awake ()
    {
        companion = GetComponent<Companion>();
        followState = new FollowState(this);
        combatState = new CombatState(this);
    }

    void Start()
    {
        currentState = followState;
    }

	void Update ()
    {
        if (Input.GetMouseButtonDown(0)) currentState.ToCombatState();
        currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
