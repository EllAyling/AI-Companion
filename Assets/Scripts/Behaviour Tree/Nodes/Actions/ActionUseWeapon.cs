using UnityEngine;
using System.Collections;

public class ActionUseWeapon : BTNode {

    private Entity entity;
    private GunController gunController;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        entity = blackboard.GetValueFromKey<Entity>("entity");
        gunController = blackboard.GetValueFromKey<GunController>("gunController");
    }

    public override NodeState Tick()
    {
        gunController.Shoot();
        return NodeState.SUCCESS;
    }
}
