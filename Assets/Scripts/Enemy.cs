using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Entity {


    public Transform[] patrolRoute;

    private AIBrain brain;
    private EnemySight eyes;
    private Blackboard blackboard;

    // Use this for initialization
    void Start () {

        eyes = GetComponentInChildren<EnemySight>();
        blackboard = new Blackboard();

        BTNode root = new SequencePatrol();

        blackboard.treeData.Add("patrolRoute", patrolRoute);
        blackboard.treeData.Add("entity", this);

        brain = new AIBrain(root, blackboard);

        brain.Start();
    }

    void Update()
    {
        brain.Update();
    }
}
