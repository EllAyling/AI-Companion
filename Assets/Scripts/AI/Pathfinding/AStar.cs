using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class AStar : MonoBehaviour {

    Grid grid;
    Transform target;
    PathfindingManager requestManager;

    void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathfindingManager>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable)  //If we can actually get the the target node
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize); 
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode); //Add the first node

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)  //Are we here?
                {
                    pathSuccess = true; //yes
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode)) //For every neighbour to our node
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))       //If the neighbour is walkable or we've already looked at it
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistanceCost(currentNode, neighbour);  //Get the cost
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) //If this new cost is shorter or the neighbour isnt in the openset
                    {
                        neighbour.gCost = newMovementCostToNeighbour; //Set the gCost
                        neighbour.hCost = GetDistanceCost(neighbour, targetNode); //Heauristic
                        neighbour.parent = currentNode; //Set the tree/path

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) //Go back through the tree
        {
            path.Add(currentNode);
            currentNode = currentNode.parent; //Create our path
        }
        Vector3[] waypoints = SimplifyPath(path); //Change to vector positions
        Array.Reverse(waypoints);
        return waypoints;

    }

	Vector3[] SimplifyPath(List<Node> path)
    {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            if (path[i].worldPos.y != path[i - 1].worldPos.y) //If the two nodes are different elevations, don't shorten, just add it. Stops flying to waypoints on different elevations
            {
                waypoints.Add(path[i].worldPos);
            }

            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY); //Otherwise get the direction between the two nodes
            if (directionNew != directionOld) //If it's a different direction
            {
                waypoints.Add(path[i].worldPos); //Add it to our waypoints
            }
            directionOld = directionNew; //Set the current(old) direction

        }
		return waypoints.ToArray();
	}

    public static int GetDistanceCost(Node nodeA, Node nodeB) //Manhatten distance
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX); //Get the x distance
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY); //Get the y distance

        if (distanceX > distanceY) return 14 * distanceY + 10 * (distanceX - distanceY); //Pyag Theourm- 14 for diagonal movement, 10 for straight movement. Square root of 2 triangle * 10.

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
