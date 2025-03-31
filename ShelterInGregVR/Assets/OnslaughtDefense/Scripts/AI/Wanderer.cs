using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Kinematic
{
    Wander myMoveType;
    Face myWanderRotateType;
    LookWhereGoing myFleeRotateType;

    void Start()
    {
        myMoveType = new Wander();
        myMoveType.character = this;
        myMoveType.target = myTarget;

        myWanderRotateType = new Face();
        myWanderRotateType.character = this;
        myWanderRotateType.target = myTarget;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myWanderRotateType.getSteering().angular;
        base.Update();
    }
}
