using UnityEngine;
using System.Collections;

public class NodeAlwaysSuccess : BTDecorator {

    public NodeAlwaysSuccess(BTNode child)
    {
        this.child = child;
    }

    public override NodeState Tick()
    {
        child.Tick();
        
        return NodeState.SUCCESS;
    }
}
