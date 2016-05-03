using UnityEngine;
using System.Collections;

public class ActionLookForEnemy : BTNode {

    private Entity entity;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
       entity.transform.Rotate(0, 80.0f * Time.deltaTime, 0);
       return NodeState.SUCCESS;   
    }
}
