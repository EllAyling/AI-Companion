using UnityEngine;
using System.Collections;

public class Player : Entity
{
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController playerControls;
    GunController gunController;

    // Use this for initialization
    public override void Start () {
        base.Start();
        EntityType type = EntityType.Player;
        AddType(type);

        playerControls = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
        gunController = GetComponent<GunController>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            gunController.Shoot();
        }
        float mouseWheelDir = Input.GetAxis("Mouse ScrollWheel");
        if(mouseWheelDir != 0.0f)
        {
            ChangeWeapon(mouseWheelDir);
        }

    }

    void ChangeWeapon(float mouseWheelDir)
    {
        if (mouseWheelDir > 0.0f)
        {
            for (int i = 0; i < gunController.allGuns.Length; i++)
            {
                if (gunController.allGuns[i].type == gunController.equippedGun.type)
                {
                    if (i != gunController.allGuns.Length - 1)
                    {
                        gunController.EquipGun(gunController.allGuns[i + 1]);
                        break;
                    }
                    else
                    {
                        gunController.EquipGun(gunController.allGuns[0]);
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < gunController.allGuns.Length; i++)
            {
                if (gunController.allGuns[i].type == gunController.equippedGun.type)
                {
                    if (i == 0)
                    {
                        gunController.EquipGun(gunController.allGuns[gunController.allGuns.Length - 1]);
                        break;
                    }
                    else
                    {
                        gunController.EquipGun(gunController.allGuns[i - 1]);
                        break;
                    }
                }
            }
        }
    }
}
