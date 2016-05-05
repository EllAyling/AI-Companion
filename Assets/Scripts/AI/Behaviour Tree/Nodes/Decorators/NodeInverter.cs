using UnityEngine;
using System.Collections;
using System;

public class NodeInverter : BTDecorator {

    public NodeInverter(BTNode child)
    {
        this.child = child;
    }

    public override NodeState Tick()
    {
        state = child.Tick();
        if (state == NodeState.SUCCESS)
        {
            state = NodeState.FAILURE;
        }
        else if (state == NodeState.FAILURE)
        {
            state = NodeState.SUCCESS;
        }
        return state;
    }

}
