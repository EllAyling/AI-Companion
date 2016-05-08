using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompanionBuff : Entity
{
    public Player player;

    private AIBrain brain;

    private Quaternion fixedRotation;
    private new GameObject light;

    public override void Start()
    {
        base.Start();
        EntityType type = EntityType.Companion;
        AddType(type);

        blackboard.treeData.Add("companion", this);

        NodeSelector root = new NodeSelector(new BTNode[]
            {
                    new ActionCheckBuff(),
                    new NodeCounter(
                        new ActionToggleBuff(), player.buffTime * 3
                        ),
                    new ActionChangeColour()
            });

        brain = new AIBrain(root, blackboard);
        brain.Start();

        fixedRotation = transform.rotation;
        light = transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPosition();
        ChangeColour();
        brain.Update();    
    }

    void CheckPosition()
    {
        transform.rotation = fixedRotation;

        transform.position = player.transform.position;
        transform.Translate(0.24f, 0.0f, 1.0f);

        var target = Quaternion.LookRotation((light.transform.position + player.transform.forward) - transform.position, Vector3.up);
        light.transform.rotation = Quaternion.Slerp(light.transform.rotation, target, 1.0f * Time.deltaTime);
    }

    void ChangeColour()
    {
        var material = GetComponent<Renderer>().material;
        if (player.buffed)
        {
            material.color = Color.Lerp(Color.green, Color.red, player.buffTimer);
        }
    }
}
