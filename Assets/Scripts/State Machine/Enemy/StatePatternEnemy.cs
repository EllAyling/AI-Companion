using UnityEngine;
using System.Collections;

public class StatePatternEnemy : Enemy {

    public float seachingTurnSpeed = 120.0f;
    public float searchingDuration = 4.0f;
    public Transform[] wayPoints;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public MeshRenderer meshRendererFlag;

    [HideInInspector] public Vector3 chaseTarget;
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public EnemySight eyes;

    // Use this for initialization
    void Awake ()
    {
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);

        grid = GameObject.Find("GameController").GetComponent<Grid>();
        eyes = GetComponentInChildren<EnemySight>();
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
