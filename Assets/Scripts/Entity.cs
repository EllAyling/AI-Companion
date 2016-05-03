using UnityEngine;
using System.Collections;

public enum EntityType
{
    Entity = 0,
    Player = 1 << 0,
    Companion = 1 << 1,
    Enemy = 1 << 2,
    PatrolEnemy = 1 << 3,
    MeleeEnemy = 1 << 4,
}

public class Entity : MonoBehaviour, IDamagable {

    public Grid grid;

    bool dead;

    public float startingHealth = 10f;
    protected float health;
    public int type = 0;
    public bool enemyNearby = false;
    public bool getSearchPosition = false;
    public bool searchingForEnemy = false;

    Vector3[] path;
    protected float pathRequestRefresh = 0.8f;
    bool pathRequested;
    public bool isOnPath;
    
    public Vector3 moveTarget;
    public Vector3 attackTarget;

    int targetIndex;
    public float speed;

    GameObject WayPoints;

    // Use this for initialization
    public virtual void Start () {
        AddType(EntityType.Entity);

        dead = false;

        pathRequested = false;
        isOnPath = false;

        health = startingHealth;

        grid = GameObject.Find("GameController").GetComponent<Grid>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void AddType(EntityType t)
    {
        type = type | (int)t;
    }
    public void RemoveType(EntityType t)
    {
        type = type & ~((int)t);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && !dead)
        {
            StopPath();
            path = newPath;
            StartCoroutine("FollowPath");
        }
    }

    public void RequestNewPath(Transform _target)
    {
        if (!pathRequested && !dead)
            StartRequest(_target.position);
    }

    public void RequestNewPath(Vector3 _target)
    {
        if (!pathRequested && !dead)
            StartRequest(_target);
    }

    public void StopPath()
    {
        if (!dead)
        {
            StopCoroutine("FollowPath");
            path = null;
            isOnPath = false;
        }
    }

    void StartRequest(Vector3 _target)
    {
        pathRequested = true;
        PathfindingManager.RequestPath(transform.position, _target, OnPathFound);
        pathRequested = false;
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            targetIndex = 0;
            Vector3 currentWaypoint = path[0];
            isOnPath = true;
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        isOnPath = false;
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                Node nodeWalkingOn = grid.NodeFromWorldPoint(transform.position);
                float walkingSpeed = speed - nodeWalkingOn.movementPenalty;
                if (walkingSpeed <= 0.0) speed = 0.1f;
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, walkingSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyNearby = false;
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject);
        }
    }
}
