using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionGetFollowPosition : BTNode
{

    private Player player;

    private Companion companion;

    private List<Vector3> candidatePositions;

    //Variables to find positions in a semi circle around the player.
    public int targetRadiusSegments = 5;
    public float targetRadiusX = 2.0f;
    public float targetRadiusY = 2.0f;

    public override void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;

        candidatePositions = new List<Vector3>();
        companion = blackboard.GetValueFromKey<Companion>("companion");
        player = companion.player;
    }

    public override NodeState Tick()
    {
        GetCandidatePositions();
        if (candidatePositions.Count == 0)
        {
            return NodeState.FAILURE;
        }

        int x = Random.Range(0, candidatePositions.Count);
        Vector3 newTarget = candidatePositions[x];
        blackboard.SetValue("target", newTarget);

        companion.transform.LookAt(newTarget);

        companion.candidatePositionsForDebug = candidatePositions;

        return NodeState.SUCCESS;
    }

    public void GetCandidatePositions()
    {
        //Get positions in a semi circle behind the player
        Vector3[] positionsToCheck = FindPlayerRadiusPositions();

        if (positionsToCheck.Length > 0)
        {
            //Clear the list
            candidatePositions.Clear();

            //Go through positions generated from the semi circle
            for (int i = 0; i < positionsToCheck.Length; i++)
            {
                //If there is a clear raycast to it i.e. No objects in the way to it and the player
                if (!Physics.Linecast(player.transform.position, positionsToCheck[i]))
                {
                    //Get the forward direction from the player
                    Vector3 forwardDirection = player.transform.forward.normalized;
                    //Get a position in 'front' of the potential position
                    Vector3 forwardPosition = positionsToCheck[i] + (forwardDirection * 2.0f);

                    //If there's isn't anything directly in front of this position
                    if (!Physics.Linecast(positionsToCheck[i], forwardPosition))
                    {
                        //If theres a clear line to a forward position to the player. So the companion doesn't place obstacle between itself and the player. Feels more natural.
                        if (!Physics.Linecast(player.transform.position, forwardPosition))
                        {
                            Node node = companion.grid.NodeFromWorldPoint(positionsToCheck[i]);
                            Vector3 difference = node.worldPos - player.transform.position;
                            if (Mathf.Abs(difference.y) < 2.0f) //If the position is on a close enough elevation to the player (i.e. stay on the same level)
                            {
                                //Add it to our potential positions to move to.
                                candidatePositions.Add(positionsToCheck[i]);
                            }
                        }
                    }
                }
            }
        }
    }

    Vector3[] FindPlayerRadiusPositions()
    {
        //Create an empty array for possible positions I can move to.
        Vector3[] radiusPositions = new Vector3[targetRadiusSegments + 1];

        //Get player position and rotation.
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = player.transform.position.z;
        float angle = player.transform.localRotation.eulerAngles.y;

        //Draw a semi circle behind the player
        for (int i = 0; i < (targetRadiusSegments + 1); i++)
        {
            x -= (Mathf.Sin(Mathf.Deg2Rad * angle) * targetRadiusX);
            z -= (Mathf.Cos(Mathf.Deg2Rad * angle) * targetRadiusY);

            //Store each position in our array
            radiusPositions[i] = new Vector3(x, y, z);

            //180 for a semi circle.
            angle += 180f / targetRadiusSegments;
        }

        //Calculate the distance between the player(the start of the semicircle), and the final point of the semi circle and then divide by 2 to get the players distance to the centre of the semi circle. 
        float centreOffset = Vector3.Distance(radiusPositions[targetRadiusSegments], player.transform.position) / 2;

        //Go through all the positions
        for (int i = 0; i < (targetRadiusSegments + 1); i++)
        {
            //Move them to put the player in the centre of the semi circle.
            radiusPositions[i].x += player.transform.right.x * centreOffset;
            radiusPositions[i].z += player.transform.right.z * centreOffset;
        }

        return radiusPositions;
    }
}
