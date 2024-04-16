using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpState : State
{//Estado en el que recoge la botella ya lanzada
    public override void Do()
    {
        directionMovement = (positionBottle - transform.position).normalized;
        rb.velocity = directionMovement * speedValue;
        isComplete = true;
    }
}
