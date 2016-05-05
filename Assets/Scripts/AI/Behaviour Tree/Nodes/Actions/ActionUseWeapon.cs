using UnityEngine;
using System.Collections;

public class ActionUseWeapon : BTNode {

    private GunController gunController;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        gunController = blackboard.GetValueFromKey<GunController>("gunController");
    }

    public override NodeState Tick()
    {
        gunController.Shoot();
        return NodeState.SUCCESS;
    }
}
