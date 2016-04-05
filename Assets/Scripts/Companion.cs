using UnityEngine;
using System.Collections;

public class Companion : Entity {

    public Player player;
    Grid grid;

    float minimumDistance;
    float maximumDistance;

	// Use this for initialization
	void Start () {
        EntityType type = EntityType.Companion;
        AddType(type);

        minimumDistance = 30;
        maximumDistance = 200;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) RequestNewPath(player.transform);
	}

    void CheckDistance()
    {
        if (AStar.GetDistance(grid.NodeFromWorldPoint(transform.position), grid.NodeFromWorldPoint(player.transform.position)) > maximumDistance)
        {           
            float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            distanceFromPlayer -= minimumDistance;
            distanceFromPlayer /= 2;
            Vector3 targetPos    = new Vector3(player.transform.position.x - distanceFromPlayer, 1.0f, player.transform.position.z - distanceFromPlayer);
            RequestNewPath(targetPos);
        }
    }
}
