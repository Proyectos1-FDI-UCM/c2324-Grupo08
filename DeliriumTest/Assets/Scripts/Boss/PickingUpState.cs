using UnityEngine;

public class PickingUpState : State
{//Estado en el que recoge la botella ya lanzada
    [SerializeField] private float speedValue;
    [SerializeField] ShootingState shootingState;
    public override void Do()
    {

        if (shootingState._bulletHit)
        {
            _myTransform.position = shootingState._bullet.transform.position;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        isComplete = true;
    }
}