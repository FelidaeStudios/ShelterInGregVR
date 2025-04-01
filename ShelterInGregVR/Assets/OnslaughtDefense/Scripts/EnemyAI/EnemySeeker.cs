using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeeker : EnemyKinematic
{
    EnemySeek myMoveType;
    EnemyFace myFaceRotateType;
    //LookWhereGoing myFleeRotateType;

    public bool flee = false;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new EnemySeek();
        myMoveType.character = this;
        myMoveType.targetTag = myTarget;
        myMoveType.flee = flee;

        myFaceRotateType = new EnemyFace();
        myFaceRotateType.character = this;
        myFaceRotateType.targetTag = myTarget;

        /*myFleeRotateType = new LookWhereGoing();
        myFleeRotateType.character = this;
        myFleeRotateType.target = myTarget;*/
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        //steeringUpdate.angular = flee ? myFleeRotateType.getSteering().angular : myFaceRotateType.getSteering().angular;
        base.Update();
    }
}
