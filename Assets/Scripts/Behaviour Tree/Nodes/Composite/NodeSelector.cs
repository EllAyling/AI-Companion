using UnityEngine;
using System.Collections;

public class NodeSelector : BTCompositor {

    int nodeRunning;

    public NodeSelector(BTNode[] children)
    {
        this.children = children;
    }

    public override NodeState Tick()
    {
        foreach(BTNode child in children)
        {
            state = child.Tick();

            if (state == NodeState.SUCCESS)
            {
                return state;
            }
        }

        return NodeState.FAILURE;
    }
}
