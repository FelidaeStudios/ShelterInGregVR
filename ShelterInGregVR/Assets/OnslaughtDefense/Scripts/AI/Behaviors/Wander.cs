using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Seek
{
    //float wanderOffset;
    //float wanderRadius;
    float wanderRate = 3;
    float wanderOrientation = 0;
    float maxAcceleration = 10f;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        wanderOrientation += Random.insideUnitCircle.x * wanderRate;
        //targetOrientation = wanderOrientation + character.transform.eulerAngles.y;
        Vector3 target = getTargetPosition(); //inheritence
        //target += wanderRadius * (targetOrientation * Vector3.one);
        result.linear = wanderOrientation * character.transform.position;
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.angular = 0;
        return result;
    }
}
