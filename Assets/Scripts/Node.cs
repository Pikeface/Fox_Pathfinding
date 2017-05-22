using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public bool walkable;
    public Vector3 position;
    public int gridX, gridZ; 
    public int gCost, hCost; // Heuristic
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }

    }
    public Node parent; 


    /// <summary>
    /// constructor for Node
    /// </summary>
    /// <param name="walkable">Detects whether node is walkable </param>
    /// <param name="position">Point where node is located </param>
    /// <param name="gridX"> X coordinate in 2D array </param>
    /// <param name="gridZ"> Z coordinate in 2D array </param>
    //constructor 
    public Node(bool walkable, Vector3 position, int gridX, int gridZ)
    {
        this.walkable = walkable;
        this.position = position;
        this.gridX = gridX;
        this.gridZ = gridZ;
    }


}



