using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeSequencer : BTCompositor {

    int nodeRunning;

    public NodeSequencer(BTNode[] children)
    {
        this.children = children;
    }

    public override NodeState Tick()
    {
        foreach(BTNode child in children)
        {
            state = child.Tick();

            if (state == NodeState.FAILURE)
            {
                return state;
            }
        }

        return NodeState.SUCCESS;
    }
}
