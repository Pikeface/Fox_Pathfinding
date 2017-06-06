using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GGL;

public class PathFollowing : SteeringBehaviour
{

    public Transform target;
    // Distance to current node
    public float nodeRadius = 5f;
    // Distance to end node
    public float targetRadius = 3f;
    private Graph graph;
    private int currentNode = 0;
    private bool isAtTarget = false;
    private List<Node> path;


    void Start()
    {
        // SET graph to FindObjectOfType Graph
        graph = FindObjectOfType<Graph>();
        // IF graph is null 
        if (graph == null)
        {
            // CALL Debug.LogError() and pass an Error message
            Debug.LogError(graph);
        }
        else
        {
            // CALL Debug.Break() (pause the editor)
            Debug.Break();
        }

    }
    public void UpdatePath()
    {
        // SET path to graph.FindPath() and pass transforms position, target's position
        path = graph.FindPath(transform.position, target.position);
        // SET currentNode to zero
        currentNode = 0;
    }

    #region SEEK
    // Special version of Seek that take into account the node radius & target radius
    Vector3 Seek(Vector3 target)
    {
        // SET force to zero
        Vector3 force = Vector3.zero;

        // SET desiredforce to target - transforms' position
        Vector3 desiredForce = target - transform.position;

        // SET desiredForce.y to zero
        desiredForce.y = 0;

        // SET distance to zero
        float distance = 0;

        // IF isAtTarget is true, NOTE: This needs to be done in a ternary operator
        // SET distance to targetRadius
        // ELSE
        // SET distance to nodeRadius

        distance = isAtTarget ? targetRadius : nodeRadius;

        // IF desiredForce's length is greater than distance
        if (desiredForce.magnitude > distance)
            // SET desiredForce to desiredForce.normalized * weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce - owner's velocity
            force = desiredForce - owner.velocity;

        // RETURN force
        return force;
    }
    #endregion

    #region GetForce
    // Calculates force for behaviour
    public override Vector3 GetForce()
    {
        // SET force to zero
        Vector3 force = Vector3.zero;

        // IF path is not null AND path count is greater than zero
        if (path != null && path.Count > 0)
        {
            // SET currentPos to path[currentNode] position
            Vector3 currentPos = path[currentNode].position;
            // IF distance between transform's position and currentPos is less than or equal to nodeRadius
            if (transform.position > currentPos)  
            {
                // Increment currentNode
                currentNode++;
            }
            
            // IF currentNode is greater than or equal to path.count
            if (currentNode > path.Count)
            {
                // SET currentNode to path.Count -1 
                currentNode = path.Count - 1;
            }
            else
            {
                // SET force to Seek() and pass currentPos
                force = Seek(currentPos);
            }
            

            #region GIZMOS
            // SET prevPosition to path[0].position

            // FOREACH node in path

            // CALL GizmoGL.AddSphere() and pass node's position, graph's nodeRadius, identity, any colour

            // CALL GizmoGL.AddLine() and pass prev, node's position, 0.1f, 0.1f, any color, any color

            // SET prev to node's position

            #endregion

            // RETURN force
            return force;


    }
    #endregion
}





