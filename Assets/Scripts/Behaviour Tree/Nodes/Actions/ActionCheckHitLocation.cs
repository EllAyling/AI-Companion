using UnityEngine;
using System.Collections;

public class ActionCheckHitLocation : BTNode {

    private Entity entity;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");

    }

    public override NodeState Tick()
    {
        if (entity.directionOfHit != Vector3.zero)
        {
            Vector3 targetLookat = new Vector3(entity.directionOfHit.x, entity.transform.position.y, entity.directionOfHit.z);
            entity.transform.LookAt(targetLookat);
            Vector3 searchPosition = entity.transform.forward * 10.0f;
            blackboard.SetValue<Vector3>("target", searchPosition);

            entity.searchingForEnemy = true;

            entity.directionOfHit = Vector3.zero;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
