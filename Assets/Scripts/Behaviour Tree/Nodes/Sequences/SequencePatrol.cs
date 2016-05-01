using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SequencePatrol : NodeSequencer {

    public SequencePatrol()
    {
        children = new BTNode[]
        {
            new ActionReachedTarget(),
            new ActionGetNextWaypoint(),
            new ActionMoveTo()
        };
    }
}
