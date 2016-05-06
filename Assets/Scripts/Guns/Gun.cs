using UnityEngine;
using System.Collections;


public enum GunType { SingleFire, Area, Melee};
public class Gun : MonoBehaviour {

    public Transform muzzle;
    public Bullet projectile;
    public ExplodingBullet exBullet;
    public float msBetweenShot = 100f;
    public float muzzleVelocity = 35f;
    public GunType type;
    public Animation attackAnimation;
    public AudioSource fireSound;
    public int damage;

    float nextShotTime;

    public void Start()
    {
        attackAnimation = GetComponent<Animation>();
        fireSound = GetComponent<AudioSource>();
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
            if (fireSound != null)
            {
                fireSound.Play();
            }

            if (type == GunType.Area)
            {
                ExplodingBullet newProjectile = Instantiate(exBullet, muzzle.position, muzzle.rotation) as ExplodingBullet;
                newProjectile.SetSpeed(muzzleVelocity);
            }
            else
            {
                Bullet newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Bullet;
                newProjectile.SetSpeed(muzzleVelocity);
                newProjectile.SetDamage(damage);

                if (type == GunType.Melee)
                {
                    newProjectile.lifeTime = 0.1f;
                    Renderer rend = newProjectile.GetComponent<Renderer>();
                    rend.enabled = false;
                }
            }


        }
    }
}
