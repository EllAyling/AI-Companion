using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110.0f;
    public List<GameObject> enemiesInSight;
    public Transform spottedEnemyPosition;

    private SphereCollider col;
    private GameObject player;
    private GameObject companion;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        companion = GameObject.FindGameObjectWithTag(Tags.companion);
        col = GetComponent<SphereCollider>();
        enemiesInSight = new List<GameObject>();
    }

    void Update()
    {
        if (enemiesInSight.Count > 0)
        {
            GameObject target = TargetToPrioritise();
            spottedEnemyPosition = target.transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player || other.gameObject == companion)
        {
            enemiesInSight.Remove(other.gameObject);
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
                }      
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player || other.gameObject == companion)
        {
            enemiesInSight.Remove(other.gameObject);
        }
    }

    GameObject TargetToPrioritise()
    {
        GameObject closestEnemy = enemiesInSight[0];

        foreach (GameObject enemy in enemiesInSight) //If any of them are the player, dont even bother checking distance. Target player.
        {
            if (enemy.gameObject == player)
            {
                return enemy.gameObject;
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
