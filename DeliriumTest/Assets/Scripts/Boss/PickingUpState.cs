using UnityEngine;

public class PickingUpState : State
{
    //Estado en el que recoge la botella ya lanzada
    [SerializeField] private float speedValue;
    [SerializeField] ShootingState shootingState;
    
    public override void Enter()
    {
        _animation.SetBool("DashState", true);
        _animation.SetBool("IdleState", false);
        _animation.SetBool("AttackState", false);
        _animation.SetBool("EscapingState", false);
    }
    
    //Se recoge la posición de la botella y si ha llegado a imapctar, cuando hace la comprobacón la posición del jefe pasa a ser el de la botella
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