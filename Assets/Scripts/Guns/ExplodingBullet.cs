using UnityEngine;
using System.Collections;

public class ExplodingBullet : MonoBehaviour {

    float speed = 10f;
    float damage = 5f;
    public float lifeTime = 3;
    public float explodeRadius = 5.0f;
    public GameObject explosion;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.1f, transform.forward, out hit, 10))
        { 
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        Collider[] colliders = Physics.OverlapSphere(hit.point, explodeRadius);
        Instantiate(explosion, hit.point, Quaternion.identity);

        foreach (Collider col in colliders)
        {
            IDamagable damageableObject = col.GetComponent<IDamagable>();
            if (damageableObject != null)
            {
                damageableObject.TakeHit(damage, hit);
            }
        }
        Destroy(gameObject);
    }
}
