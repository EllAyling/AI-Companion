using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum Directions
{
    NORTH = 0,
    NORTH_EAST = 1,
    EAST = 2,
    SOUTH_EAST = 3,
    SOUTH = 4,
    SOUTH_WEST = 5,
    WEST = 6,
    NORTH_WEST = 7,
}

public class Grid : MonoBehaviour {

    public bool displayGrid;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public TerrainType[] walkableRegions;

    private LayerMask walkableMask;
    private Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();
    private Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        foreach (TerrainType region in walkableRegions)
        {
            walkableMask.value = walkableMask | region.terrainMask.value;
            walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask.value, 2), region.terrainPenalty);
        }

        CreateGrid();
    }

    public int MaxSize {
        get { return gridSizeX * gridSizeY; }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = (transform.position - (Vector3.right * gridWorldSize.x / 2) - (Vector3.forward * gridWorldSize.y / 2));
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius); //Get the world point

                while (Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)) //Cast a sphere the size of a node, and check to see if theres an unwalkable object there.
                {
                    worldPoint += Vector3.up * nodeRadius;  //Whilst there is, move up a node until there isn't.
                }

                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)); //Mark it walkable.

                int movementPenalty = 0;
                if (walkable)
                {
                    Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, walkableMask))
                    {
                        walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                    }
                }

                grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
            }
        }
    }
    
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    Vector3 difference = node.worldPos - grid[checkX, checkY].worldPos;
                    if (Mathf.Abs(difference.y) < nodeDiameter)
                    { 
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null && displayGrid)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;               
                Gizmos.DrawCube(node.worldPos, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}

[System.Serializable]
public class TerrainType
{
    public LayerMask terrainMask;
    public int terrainPenalty;

}
