using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AISight : MonoBehaviour {

    public float fieldOfViewAngle = 110.0f;
    public List<GameObject> enemiesInSight;
    public Transform spottedEnemyPosition;

    public Entity parentEntity;

    public SphereCollider col;

    void Start()
    {
        col = GetComponent<SphereCollider>();
        enemiesInSight = new List<GameObject>();
        parentEntity = transform.parent.GetComponent<Entity>();
    }

    void Update()
    {
        if (enemiesInSight.Count > 0)
        {
            GameObject target = TargetToPrioritise();
            spottedEnemyPosition = target.transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((parentEntity.type & (int)EntityType.Companion) != 0)
        {
            if (other.gameObject.GetComponent<MedKit>() is MedKit)
            {
                if (!parentEntity.seenMedKits.Contains(other.gameObject.transform.position))
                {
                    parentEntity.seenMedKits.Add(other.gameObject.transform.position);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (parentEntity.enemiesInLevel.Contains(other.gameObject))
        {
            if (!enemiesInSight.Contains(other.gameObject))
            {
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle < fieldOfViewAngle * 0.5f)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))
                    {
                        if (hit.collider.gameObject == other.gameObject)
                        {
                            enemiesInSight.Add(hit.collider.gameObject);
                        }
                        else
                        {
                            enemiesInSight.Remove(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (parentEntity.enemiesInLevel.Contains(other.gameObject))
        {
            enemiesInSight.Remove(other.gameObject);
        }
    }

    GameObject TargetToPrioritise()
    {
        GameObject closestEnemy = enemiesInSight[0];

        if ((parentEntity.type & (int)EntityType.Enemy) != 0) //If we're an enemy...
        {
            foreach (GameObject enemy in enemiesInSight) //If any of them are the player, dont even bother checking distance. Target player.
            {
                if ((enemy.GetComponent<Entity>().type & (int)EntityType.Player) != 0)
                {
                    return enemy.gameObject;
                }
            }
        }

        foreach (GameObject enemy in enemiesInSight) //Otherwise get the closest enemy.
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < Vector3.Distance(transform.position, closestEnemy.transform.position))
            {
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
        
    }
}
