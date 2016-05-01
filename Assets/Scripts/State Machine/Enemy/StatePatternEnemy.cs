using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public EnemySM enemy;

    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public PatrolState patrolState;

    // Use this for initialization
    void Awake ()
    {
        enemy = GetComponent<EnemySM>();

        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);

	}

    void Start()
    {
        currentState = patrolState;
        currentState.FromChaseState();
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
