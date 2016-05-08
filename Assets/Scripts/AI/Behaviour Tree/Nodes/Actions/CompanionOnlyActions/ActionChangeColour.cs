using UnityEngine;
using System.Collections;

public class ActionChangeColour : BTNode {

    private CompanionBuff companion;
    Material material;
    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        companion = blackboard.GetValueFromKey<CompanionBuff>("companion");
        material = companion.GetComponent<Renderer>().material;
    }

    public override NodeState Tick()
    {
        float newG = material.color.g;
        newG += 0.15f * Time.deltaTime;
        material.color = new Color(material.color.r, newG, material.color.b);
        return NodeState.SUCCESS;
    }
}
