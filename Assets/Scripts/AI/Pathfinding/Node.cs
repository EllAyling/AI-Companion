using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {

    public bool walkable;
    public Vector3 worldPos;
    public int gridX;
    public int gridY;
    public int movementPenalty;

    //A* Variables
    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, int _penalty)
    {
        walkable = _walkable;
        worldPos = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _penalty;
    }

    public int fCost { get { return gCost + hCost; } }

    public int HeapIndex { get; set; }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)       //If the fCosts are equal
        {
            compare = hCost.CompareTo(nodeToCompare.hCost); //Compare the hcost
        }
        return -compare;    //Return negative compare to return positive 1 if LOWER
    }
}

