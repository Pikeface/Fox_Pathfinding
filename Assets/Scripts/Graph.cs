using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Graph : MonoBehaviour
{

    public float nodeRadius = 1f;
    public LayerMask unwalkableMask;
    public Node[,] nodes;

    private float nodeDiameter;
    private int gridSizeX, gridSizeZ;
    private Vector3 scale;
    private Vector3 halfScale;


    // Use this for initialization
    void Start()
    {
        CreateGrid();
    }

    void Update()
    {
        //CreateGrid();
        CheckWalkables();
    }

    public Node GetNodeFromPosition(Vector3 position)
    {
        // calculate percenatage of grid posiotion
        float percentX = (position.x + halfScale.x) / scale.x;
        float percentZ = (position.z + halfScale.z) / scale.z;

        // clamp percentage  to a 0-1 ratio
        percentX = Mathf.Clamp01(percentX);
        percentZ = Mathf.Clamp01(percentZ);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int z = Mathf.RoundToInt((gridSizeZ - 1) * percentZ);

        Node node = nodes[x, z];

        if (!node.walkable)
            return FindClosestWalkable(node);
        
        // return the node at translated coordinate
        return node;

    }

    public Node FindClosestWalkable(Node node)
    {
        for (int i = 0; i < gridSizeX * gridSizeZ; i++)
        {
            List<Node> neighbours = new List<Node>();
            neighbours = GetNeighbours(node);
            foreach (Node neighbour in neighbours)
            {
                if (neighbour.walkable)
                    return neighbour;
            }

        }
        return null;
    }

    public List<Node> GetNeighbours(Node node)
    {
        // Make new list of neigbours 
        List<Node> neighbours = new List<Node>();
        // Try and look at surrounding neighbours 

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                // if the current index s the node being checked 
                if (x == 0 && z == 0)
                    continue;
                // check the current surrounding node
                int checkX = node.gridX + x;
                int checkZ = node.gridZ + z;
                // check if index is out of bounds
                if (checkX >= 0 &&
                    checkX < gridSizeX &&
                    checkZ >= 0 &&
                    checkZ < gridSizeZ)
                {
                    // add the neighbour to the list 
                    neighbours.Add(nodes[checkX, checkZ]);
                }
            }
        }

        // return neighbour
        return neighbours;
    }



    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
        // check if node have been created
        if (nodes != null)
        {
            // Loop through all the nodes
            for (int x = 0; x < nodes.GetLength(0); x++)
            {
                for (int z = 0; z < nodes.GetLength(1); z++)
                {
                    // Get the node and store it in a variable
                    Node node = nodes[x, z];

                    /// set the color f gizmos for node depending on walkable
                    /// 
                    Gizmos.color = node.walkable ? new Color(0, 0, 1, 0.5f) :
                                                    new Color(1, 0, 0, 0.5f);

                    // Draw a sphere to represent node  
                    Gizmos.DrawSphere(node.position, nodeRadius);


                }
            }
        }
    }


    // Generates a grid on hte x and z axis
    public void CreateGrid()
    {
        nodeDiameter = nodeRadius * 2f;

        // get transforms scale
        scale = transform.localScale;

        // half the scale 
        halfScale = scale / 2f;

        // calculate the grid size in (int) form (Mathf rounds the int)
        gridSizeX = Mathf.RoundToInt(scale.x / nodeDiameter);
        gridSizeZ = Mathf.RoundToInt(scale.z / nodeDiameter);

        // Create the 2D array of grid sizes calulated 
        nodes = new Node[gridSizeX, gridSizeZ];

        // get the ottom left point of the graph
        Vector3 bottomLeft = transform.position - Vector3.right * halfScale.x - Vector3.forward * halfScale.z;

        // Loop through all nodes in grid 
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                // calculate offset for X and Z 
                float xOffset = x * nodeDiameter + nodeRadius;
                float zOffset = z * nodeDiameter + nodeRadius;
                // create position using offsets 
                Vector3 nodePos = bottomLeft + Vector3.right * xOffset + Vector3.forward * zOffset;

                // use physics to check if node collided with walkable object
                bool walkable = !Physics.CheckSphere(nodePos, nodeRadius, unwalkableMask);
                // create node and place in 2D array coordinate
                nodes[x, z] = new Node(walkable, nodePos, x, z);
            }
        }


    }

    // Update is called once per frame
    void CheckWalkables()
    {
        // Loop through all the nodes 
        for (int x = 0; x < nodes.GetLength(0); x++)
        {
            for (int z = 0; z < nodes.GetLength(1); z++)
            {
                // grab node at index
                Node node = nodes[x, z];
                // check if node collided with non-walkable
                node.walkable = !Physics.CheckSphere(node.position, nodeRadius, unwalkableMask);
            }
        }
    }

}
