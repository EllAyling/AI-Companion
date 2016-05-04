using UnityEngine;
using System.Collections;

public class EnemySM : Entity {

    public AISight eyes;

    public float seachingTurnSpeed = 120.0f;
    public float searchingDuration = 4.0f;
    public Transform[] wayPoints;
    public MeshRenderer meshRendererFlag;

    [HideInInspector] public Vector3 chaseTarget;
    [HideInInspector] public StatePatternEnemy statemachine;
    // Use this for initialization
    public override void Start () {
        base.Start();
        EntityType type = EntityType.Enemy;
        AddType(type);
        speed = 2;

        eyes = GetComponentInChildren<AISight>();
        statemachine = GetComponent<StatePatternEnemy>();
    }

    void Update()
    {
        
    }
}
