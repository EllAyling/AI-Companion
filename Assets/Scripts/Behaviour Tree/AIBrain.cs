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
    private List<BTNode> treeNodes;

    public AIBrain(BTNode root, Blackboard blackboard)
    {
        this.root = root;
        this.blackboard = blackboard;
        blackboard.SetValue<List<BTNode>>("treeNodes", treeNodes);
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
