using UnityEngine;
using System.Collections;

public class Player : Entity
{
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController playerControls;
    public Companion companion;
    GunController gunController;
    float defaultSpeed;

    public Entity enemyLastFiredAt;

    public int medKitValue = 2;

    public bool buffed;
    [HideInInspector] public float buffTimer;
    public float buffTime = 6.0f;
    public float buffSpeedBoost;

    // Use this for initialization
    public override void Start () {
        base.Start();
        EntityType type = EntityType.Player;
        AddType(type);

        playerControls = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
        gunController = GetComponent<GunController>();

        defaultSpeed = playerControls.movementSettings.ForwardSpeed;

        buffed = false;
        buffTimer = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            gunController.Shoot();
            RaycastHit hit;
            if (Physics.Raycast(gunController.equippedGun.muzzle.transform.position, gunController.equippedGun.muzzle.transform.forward, out hit, 50.0f, enemiesMask, QueryTriggerInteraction.UseGlobal))
            {
                enemyLastFiredAt = hit.collider.gameObject.GetComponent<Entity>();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (medKits > 0 && health != startingHealth)
            {
                health += medKitValue;
                medKits -= 1;

                if (health > startingHealth)
                {
                    health = startingHealth;
                }
            }
        }
        float mouseWheelDir = Input.GetAxis("Mouse ScrollWheel");
        if(mouseWheelDir != 0.0f)
        {
            ChangeWeapon(mouseWheelDir);
        }

        Node nodeWalkingOn = grid.NodeFromWorldPoint(transform.position);

        float addedSpeed = 0.0f;
        if (buffed)
        {
            addedSpeed = buffSpeedBoost;
            buffTimer += Time.deltaTime;
            if (buffTimer > buffTime)
            {
                buffed = false;
                buffTimer = 0.0f;
            }
        }

        float speed = (defaultSpeed + addedSpeed) - nodeWalkingOn.movementPenalty;
        playerControls.movementSettings.ForwardSpeed = speed;
    }

    void OnTriggerEnter(Collider other)
    {
        Companion companion = other.gameObject.GetComponent<Companion>();
        if(companion)
        {
            if (companion.medKits > 0)
            {
                medKits += companion.medKits;
                companion.medKits -= companion.medKits;
            }
            else if (companion.ammo > 0)
            {
                ammo += companion.ammo;
                companion.ammo -= companion.ammo;
            }
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
