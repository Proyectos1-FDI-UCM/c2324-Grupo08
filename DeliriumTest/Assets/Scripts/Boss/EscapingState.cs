using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapingState : State
{//Estado en el que huye del jugador si se acerca
    private FrankMovement frankDirection;
    private void OnTriggerStay2D(Collider2D collision)
    {
        FrankMovement player =collision.GetComponent<FrankMovement>();
        if(player != null)
        {
            currentSpeed = speedValue;
            directionMovement = frankDirection.Direction.normalized;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            currentSpeed = 0;
        }
    }
    public override void Enter()
    {
        frankDirection = FrankMovement.Player.GetComponent<FrankMovement>();
        currentSpeed = 0;
    }
    public override void Do()
    {
        rb.velocity = directionMovement * currentSpeed;
    }
}
