using UnityEngine;
using System.Collections;

public class ActionGiveMedKitToPlayer : BTNode
{
    Companion companion;

    // Use this for initialization
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("entity");
    }

    public override NodeState Tick()
    {
        if (companion.medKits > 0)
        {
            Debug.Log("GOing to player");
            Vector3 target = companion.player.transform.position - companion.player.transform.forward * 2.0f;
            blackboard.SetValue("target", target);
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
