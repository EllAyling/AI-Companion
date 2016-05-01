using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NodeSequencer : BTCompositor {

    public override NodeState Tick()
    {
        foreach (BTNode child in children)
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
