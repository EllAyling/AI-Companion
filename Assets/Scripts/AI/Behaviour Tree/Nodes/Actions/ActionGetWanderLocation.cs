using UnityEngine;
using System.Collections;

public class ActionGetWanderLocation : BTNode {

    Entity entity;

    // Use this for initialization
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
    }

    public override NodeState Tick()
    {
        Vector3 target = Search();
        blackboard.SetValue<Vector3>("target", target);
        entity.transform.LookAt(target);
        return NodeState.SUCCESS;
    }

    public Vector3 Search()
    {
        Vector2 target = Random.insideUnitCircle;

        Vector3 finalTarget = entity.transform.position + new Vector3(target.x, 0.0f, target.y); //Change from xy plane to xz plane
        finalTarget *= 1.4f;

        return finalTarget;
    }
}
