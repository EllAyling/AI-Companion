using UnityEngine;
using System.Collections;

public class ActionGetOverwatchPosition : BTNode {

    private Companion companion;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<Companion>("companion");
    }

    public override NodeState Tick()
    {
        Collider[] OWpos = Physics.OverlapSphere(companion.transform.position, 50.0f, GameController.gameController.overWatchPosLayer, QueryTriggerInteraction.Collide);

        Vector3 closestPos = new Vector3();
        if (OWpos.Length > 0)
        {
            closestPos = OWpos[0].gameObject.transform.position;
            foreach (Collider col in OWpos)
            {
                float distance = Vector3.Distance(companion.transform.position, col.gameObject.transform.position);
                if (distance < Vector3.Distance(companion.transform.position, closestPos))
                {
                    closestPos = col.gameObject.transform.position;
                }
            }
            blackboard.SetValue("target", closestPos);
            return NodeState.SUCCESS;
        }
        else if (OWpos.Length == 1)
        {
            closestPos = OWpos[0].gameObject.transform.position;
            blackboard.SetValue("target", closestPos);
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
