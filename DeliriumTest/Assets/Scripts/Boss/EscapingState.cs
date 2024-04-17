using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapingState : State
{//Estado en el que huye del jugador si se acerca
    [SerializeField] private float escapingValue;
    private void OnTriggerStay2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            bossMovement.currentSpeed = escapingValue;
            bossMovement.directionMovement = frankDirection.Direction.normalized;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            bossMovement.currentSpeed = 0;
        }
    }
    public override void Enter()
    {
        bossMovement.currentSpeed = 0;
    }
    public override void Do()
    {
        rb.velocity = bossMovement.directionMovement * bossMovement.currentSpeed;
        isComplete = true;
    }
}
