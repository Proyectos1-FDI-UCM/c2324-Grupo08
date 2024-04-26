using UnityEngine;

public class PickingUpState : State
{//Estado en el que recoge la botella ya lanzada
    [SerializeField] private float speedValue;
    [SerializeField] ShootingState shootingState;
    public override void Do()
    {

        if (shootingState._bulletHit)
        {
            bossMovement.directionMovement = (shootingState._bullet.transform.position - transform.position).normalized;
            rb.velocity = bossMovement.directionMovement * speedValue;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        isComplete = true;
    }
}