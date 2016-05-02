using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    public Gun equippedGun;
    public Transform weaponHold;
    public Gun startingGun;
    public Gun[] allGuns;
    
    void Start()
    {
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
        print(equippedGun.type);
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
