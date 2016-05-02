using UnityEngine;
using System.Collections;

public class ActionDebug : BTNode {

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public override NodeState Tick()
    {
        Debug.Log("Debug Node");

        return NodeState.SUCCESS;
    }
}
