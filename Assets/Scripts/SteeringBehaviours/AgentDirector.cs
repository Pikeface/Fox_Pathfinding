using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class AgentDirector : MonoBehaviour
{

    public Transform selectedTarget;
    public float rayDistance = 1000f;
    public LayerMask selectionLayer;
    private AIAgent[] agents;
    
    void Start()
    {
        // SET Agents to FindObjectsOfType AIAgent
        agents = FindObjectsOfType<AIAgent>();

    }
    
    public void ApplySelection()
    {
        // FOREACH agent in agents
        foreach (AIAgent agent in agents)
        {
            // SET pathFollowing = agent.Getcomponent<PathFollowing>();
            PathFollowing pathFollowing = agent.GetComponent<PathFollowing>();
            // IF pathFollowing is not null 
            if (pathFollowing != null)
            {
                // SET pathFollowing.target to selectedTarget
                pathFollowing.target = selectedTarget;
                // CALL pathFollowing.UpdatePath()
                pathFollowing.UpdatePath();

            }
        }
    }

    void CheckSelection()
    {
        // SET ray to ray from camera 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // SET hit to new RaycastHit 
        RaycastHit hit = new RaycastHit();
        // IF Physics.Raycast() and pass ray, out hit, rayDistance, selectionLayer
        if (Physics.Raycast(ray, out hit, rayDistance, selectionLayer))
        {
            // CALL GizmoGL.AddSphere() and pass hit.point, 5f, Quaternion.identity, ay color
            GizmosGL.AddSphere(hit.point, 5f, Quaternion.identity, Color.red);
            // IF user clicked left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // SET selectedTarget to hit.collider.transform
                selectedTarget = hit.collider.transform;
                // CALL ApplySelection
                ApplySelection();

            }
        }
    }
    void Update()
    {
        // CALL CheckSelection()
        CheckSelection();

    }

}


