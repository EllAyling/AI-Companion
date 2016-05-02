using UnityEngine;
using System.Collections;

public class NodeAlwaysFail : BTDecorator {

    public NodeAlwaysFail(BTNode child)
    {
        this.child = child;
    }

    public override NodeState Tick()
    {
        child.Tick();
        
        return NodeState.FAILURE;
    }
}
