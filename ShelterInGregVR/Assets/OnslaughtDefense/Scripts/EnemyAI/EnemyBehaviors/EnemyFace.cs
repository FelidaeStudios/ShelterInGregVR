using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFace : EnemyAlign
{
    public override float getTargetAngle()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length == 0)
        {
            return float.PositiveInfinity; // No target found, return a fallback value
        }

        // Find the closest target
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject potentialTarget in targets)
        {
            float distance = Vector3.Distance(character.transform.position, potentialTarget.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = potentialTarget;
            }
        }

        if (closestTarget != null)
        {
            Vector3 direction = closestTarget.transform.position - character.transform.position;
            direction.y = 0; // Keep movement in the X-Z plane
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            return targetAngle; // Return the calculated angle
        }

        return float.PositiveInfinity;
    }
}