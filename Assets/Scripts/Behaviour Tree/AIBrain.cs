using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum NodeState
{
    SUCCESS,
    FAILURE,
    RUNNING
}

public class AIBrain {

    public BTNode root;
    protected Blackboard blackboard;

    public AIBrain(BTNode root, Blackboard blackboard)
    {
        this.root = root;
        this.blackboard = blackboard;
    }

    public void Start()
    {
        root.Init(blackboard);
    }
	
	// Update is called once per frame
	public void Update ()
    {
        root.Tick();
	}
}
