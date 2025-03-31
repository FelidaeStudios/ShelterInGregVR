using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : Kinematic
{
    FollowPath myMoveType;
    LookWhereGoing myPathRotateType;
    public GameObject[] pathWaypoints;

    void Start()
    {
        myMoveType = new FollowPath();
        myMoveType.character = this;
        myMoveType.path = pathWaypoints;

        myPathRotateType = new LookWhereGoing();
        myPathRotateType.character = this;
        myPathRotateType.target = myTarget;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myPathRotateType.getSteering().angular;
        base.Update();
    }
}
