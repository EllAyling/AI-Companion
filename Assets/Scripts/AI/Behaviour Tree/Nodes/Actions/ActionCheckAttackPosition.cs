using UnityEngine;
using System.Collections;

public class ActionCheckAttackPosition : BTNode {

    private Entity entity;
    private AISight eyes;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
        eyes = entity.GetComponentInChildren<AISight>();
    }

    public override NodeState Tick()
    {
        float distance = Vector3.Distance(entity.transform.position, eyes.spottedEnemyPosition.position);
        if (distance < 4.0f)
        {
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
