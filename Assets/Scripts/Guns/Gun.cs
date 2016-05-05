using UnityEngine;
using System.Collections;


public enum GunType { SingleFire, Area, Melee};
public class Gun : MonoBehaviour {

    public Transform muzzle;
    public Projectile projectile;
    public ExplodingBullet exBullet;
    public float msBetweenShot = 100f;
    public float muzzleVelocity = 35f;
    public GunType type;
    public Animation attackAnimation;

    float nextShotTime;

    public void Start()
    {
        attackAnimation = GetComponent<Animation>();
        switch (type)
        {
            case (GunType.SingleFire):
                break;

            case (GunType.Area):
                break;

            case (GunType.Melee):
                break;
        }
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShot / 1000;

            if (type == GunType.Area)
            {
                ExplodingBullet newProjectile = Instantiate(exBullet, muzzle.position, muzzle.rotation) as ExplodingBullet;
                newProjectile.SetSpeed(muzzleVelocity);
            }
            else
            {
                Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);

                if (type == GunType.Melee)
                {
                    newProjectile.lifeTime = 0.1f;
                    newProjectile.SetDamage(10);
                    Renderer rend = newProjectile.GetComponent<Renderer>();
                    rend.enabled = false;
                }
            }


        }
    }
}
