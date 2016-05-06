using UnityEngine;
using System.Collections;

public class ActionMoveToPlayer : BTNode {

    Entity entity;

    // Use this for initialization
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
        Transform playerTrans = GameController.gameController.player.transform;
        Vector3 dir = entity.transform.position - playerTrans.position;
        dir.Normalize();

        Vector3 target = playerTrans.position;

        entity.transform.LookAt(target);

        blackboard.SetValue("target", target);

        return NodeState.SUCCESS;
    }
}
