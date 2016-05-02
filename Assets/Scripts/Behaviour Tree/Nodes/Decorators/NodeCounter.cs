using UnityEngine;
using System.Collections;

public class NodeCounter : BTDecorator {

    int count;
    int maxCount;

    public NodeCounter(BTNode child, int maxCount)
    {
        this.child = child;
        this.maxCount = maxCount;
        count = int.MaxValue;
    }

    public override NodeState Tick()
    {
        if (count * Time.deltaTime >= maxCount)
        {
            count = 0;
            state = child.Tick();
            return state;
        }
        else
        {
            count++;
            return NodeState.FAILURE;
        }
    }
}
