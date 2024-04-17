using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpState : State
{//Estado en el que recoge la botella ya lanzada
    [SerializeField] private float speedValue;
    public override void Do()
    {
        bossMovement.directionMovement = (bossMovement.positionBottle - transform.position).normalized;
        rb.velocity = bossMovement.directionMovement * speedValue;
        isComplete = true;
    }
}
