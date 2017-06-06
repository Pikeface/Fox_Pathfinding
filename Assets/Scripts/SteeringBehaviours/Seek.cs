﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance = 0f;

    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;

        // IF there is no target, then return force
        if (target == null) return force;

        // SET desiredForce to direction from target to position
        // force = force

        Vector3 desiredForce = target.position - transform.position;
        // SET desiredForce y to zero
        desiredForce.y = 0;

        // Vector3 direction = target.position - transform.position;

        // IF direction is greater than stopping distance 
        if (desiredForce.magnitude > stoppingDistance)
        {
            // SET desiredForce to normalized and multiply by weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce and subtract ownser's velocity
            force = desiredForce - owner.velocity;
        }

        return force;
    }
}


