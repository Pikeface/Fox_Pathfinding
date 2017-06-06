using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GGL;

public class Wander : SteeringBehaviour
{
    public float offset = 1.0f;
    public float radius = 1.0f;
    public float jitter = 0.2f;
    
    private Vector3 targetDir;
    
    public override Vector3 GetForce()
    {
        // set force to zero
        Vector3 force = Vector3.zero;

        // gernerate random numbers between a certain range

        float randX = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);
        float randZ = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);

        #region Calculate RandomDir
        // SET randomDir to new Vector3 x = randX & z = randZ
        Vector3 randomDir = new Vector3(randX, 0, randZ);

        // SET randomDir to normalized 
        randomDir = randomDir.normalized * weighting;

        // SET randomDir to randomDir x jitter (amplify randomDir by jitter)
        randomDir = randomDir * jitter;
        #endregion

        #region Calculate TargetDir
        // SET targetDir to targetDir + randomDir
        targetDir = targetDir + randomDir;

        // SET targetDir to normalized targetDir
        targetDir = targetDir.normalized;

        // SET targetDir to targetDir x radius
        targetDir = targetDir * radius;

        #endregion

        #region Calculate force
        // SET seekPos to owners position + targetDir
        Vector3 seekPos = transform.position + targetDir;

        // SET seekPos to seekPos + owner's forward x offset
        seekPos = seekPos + owner.transform.forward * offset;

        #endregion

        #region GIZMOS
        // SET offsetPos to position + forward x offset
        Vector3 offsetPos = transform.position + transform.forward.normalized * offset;
        GizmosGL.AddCircle(offsetPos + Vector3.up * 0.1f, 
                            radius,
                            Quaternion.LookRotation(Vector3.down), 
                            16, 
                            Color.red);
        GizmosGL.AddCircle(seekPos + Vector3.up * 0.15f, 
                            radius * 0.6f,
                            Quaternion.LookRotation(Vector3.down), 
                            16, Color.blue);

        // ADD circle with offsetPos + up x amount, rotate circle with LookRotation (down)

        #endregion

        #region 

        // SET desiredForce to seekPos - position
        Vector3 desiredForce = seekPos - transform.position;

        // IF desiredforce is not zero
        if (desiredForce != Vector3.zero)
        {
            // SET desiredForce to desiredForce normalized x weighting
            desiredForce = desiredForce.normalized * weighting;

            // SET force to desiredForce - owner's velocity 
            // wtf just set force...
            force = desiredForce - owner.velocity;
        }
        #endregion

        // return force
        return force;
    }




}
