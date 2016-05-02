using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110.0f;
    public bool playerInSight;
    public Transform spottedPlayerPosition;

    public LayerMask collisionMask;

    private SphereCollider col;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        col = GetComponent<SphereCollider>();
        playerInSight = false;
    }

    void Update()
    {       
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        spottedPlayerPosition = player.transform;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }
}
