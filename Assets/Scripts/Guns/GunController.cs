using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    public Gun equippedGun;
    public Transform weaponHold;
    public Gun startingGun;
    public Gun[] allGuns;
    Entity entity;
    
    void Start()
    {
        entity = GetComponent<Entity>();
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
            equippedGun = null;
        }
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.attackAnimation.Play();
            if (equippedGun.type == GunType.Area)
            {
                if (entity.ammo > 0)
                {
                    Invoke("ShootInvoke", 0.85f);
                }
                else
                {
                    Debug.Log("No ammo");
                }
            }
            else
            {
                equippedGun.Shoot();
            }
        }
    }

    public void ShootInvoke()
    {
        entity.ammo -= 1;
        equippedGun.Shoot();
    }
}
