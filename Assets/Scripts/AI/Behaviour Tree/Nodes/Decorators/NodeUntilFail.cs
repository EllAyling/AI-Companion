using UnityEngine;
using System.Collections;
using System;

public class NodeUntilFail : BTDecorator {

    public NodeUntilFail(BTNode child)
    {
        this.child = child;
    }

    public override NodeState Tick()
    {
        state = child.Tick();

        if (state != NodeState.FAILURE)
        {
            state = child.Tick();
        }

        return state;
    }

}
