using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeek : SteeringBehavior
{
    public EnemyKinematic character;
    public string targetTag = "Player";

    float maxAcceleration = 100f;
    public bool flee = false;

    protected virtual Vector3 getTargetPosition()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length == 0)
        {
            return Vector3.positiveInfinity;
        }

        GameObject target = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject potentialTarget in targets)
        {
            float distance = Vector3.Distance(character.transform.position, potentialTarget.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = potentialTarget;
            }
        }

        return target ? target.transform.position : Vector3.positiveInfinity;
        //return target.transform.position;
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        Vector3 targetPosition = getTargetPosition();
        if (targetPosition == Vector3.positiveInfinity)
        {
            return null;
        }

        // Get the direction to the target
        if (flee)
        {
            //result.linear = character.transform.position - target.transform.position;
            result.linear = character.transform.position - targetPosition;
        }
        else
        {
            //result.linear = target.transform.position - character.transform.position;
            result.linear = targetPosition - character.transform.position;
        }

        // give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}
