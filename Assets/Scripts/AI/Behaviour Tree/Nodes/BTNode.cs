using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class BTNode
{
    public abstract void Init(Blackboard blackboard);
    public abstract NodeState Tick();

    public NodeState state;
    public Blackboard blackboard;
}

public abstract class BTDecorator : BTNode
{
    public BTNode child;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        child.Init(blackboard);
    }
}

public abstract class BTCompositor : BTNode
{
    public BTNode[] children;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        foreach (BTNode child in children)
        {
            child.Init(blackboard);
        }
    }
}