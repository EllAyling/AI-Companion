using UnityEngine;
using System.Collections;

public class NodeCounter : BTDecorator {

    float count;
    float maxCount;

    public NodeCounter(BTNode child, float maxCount)
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
