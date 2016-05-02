using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SequencePatrol : NodeSequencer {
    public SequencePatrol(BTNode[] children) : base(children)
    {
        children = new BTNode[]
        {
            new ActionReachedTarget(),
            new ActionGetNextWaypoint(),
            new ActionRequestPathToTarget()
        };
    }
}
