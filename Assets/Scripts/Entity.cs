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

public class Entity : MonoBehaviour {

    public Grid grid;

    public int health;
    public int type = 0;

    Vector3[] path;
    protected float pathRequestRefresh = 0.8f;
    bool pathRequested;
    public bool isOnPath;

    public Vector3 moveTarget;
    public Vector3 attackTarget;

    int targetIndex;
    public float speed = 2;

    GameObject WayPoints;

    // Use this for initialization
    void Start () {
        AddType(EntityType.Entity);

        pathRequested = false;
        isOnPath = false;

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
        if (pathSuccessful)
        {
            StopPath();
            path = newPath;
            StartCoroutine("FollowPath");
        }
    }

    public void RequestNewPath(Transform _target)
    {
        if (!pathRequested)
            StartRequest(_target.position);
    }

    public void RequestNewPath(Vector3 _target)
    {
        if (!pathRequested)
            StartRequest(_target);
    }

    public void StopPath()
    {
        StopCoroutine("FollowPath");
        path = null;
        isOnPath = false;
    }

    void StartRequest(Vector3 _target)
    {
        pathRequested = true;
        Debug.Log("Path Request");
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
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
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
}
