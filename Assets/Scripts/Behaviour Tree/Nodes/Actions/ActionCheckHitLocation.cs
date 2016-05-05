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
            Vector3 searchPosition = entity.transform.forward * 5.0f;
            if (Physics.Linecast(entity.transform.position, searchPosition))
            {
                blackboard.SetValue("target", searchPosition);
                entity.searchingForEnemy = true;
                entity.directionOfHit = Vector3.zero;
            }
            else
            {
                blackboard.SetValue("target", entity.transform.forward * 2.0f);
                entity.searchingForEnemy = true;
                entity.directionOfHit = Vector3.zero;
            }
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
