using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    public EnemySight eyes;

    public float seachingTurnSpeed = 120.0f;
    public float searchingDuration = 4.0f;
    public Transform[] wayPoints;
    public MeshRenderer meshRendererFlag;

    [HideInInspector] public Vector3 chaseTarget;
    [HideInInspector] public StatePatternEnemy statemachine;
    // Use this for initialization
    void Start () {
        EntityType type = EntityType.Enemy | EntityType.Normal;
        AddType(type);
        speed = 2;

        eyes = GetComponentInChildren<EnemySight>();
        statemachine = GetComponent<StatePatternEnemy>();
    }

    void Update()
    {
        
    }
}
