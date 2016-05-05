using UnityEngine;
using System.Collections;

public class ActionCheckForEnemiesInSight : BTNode {

    private AISight eyes;
    private Entity entity;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        eyes = blackboard.GetValueFromKey<AISight>("eyes");
        entity = eyes.transform.parent.GetComponent<Entity>();
    }

    public override NodeState Tick()
    {
        if (eyes.enemiesInSight.Count > 0)
        {
            if (eyes.spottedEnemyPosition)
            {
                blackboard.SetValue("spottedPlayerPosition", eyes.spottedEnemyPosition);
                entity.transform.LookAt(eyes.spottedEnemyPosition.position);
                entity.getSearchPosition = true;
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
        else
        {
            eyes.enemiesInSight.Clear();
            return NodeState.FAILURE;
        }
    }
}
