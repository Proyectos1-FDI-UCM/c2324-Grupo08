using UnityEngine;

//Estado en el que recoge la botella ya lanzada
public class PickingUpState : State
{
    /// <summary>
    /// State anterior del comportamiento.
    /// </summary>
    [SerializeField] ShootingState shootingState;

    public override void Enter()
    {
        _animation.SetBool("DashState", true);
        _animation.SetBool("IdleState", false);
        _animation.SetBool("AttackState", false);
        _animation.SetBool("EscapingState", false);
    }

    /// <summary>
    /// Se recoge la posición de la botella y la posición del jefe pasa a ser el de la botella.
    /// Ademas el boss vuelve a ser vulnerable a los taques de Paco.
    /// </summary>
    public override void Do()
    {
        _myTransform.position = shootingState._bullet.transform.position;
        shootingState._bulletHit = false;
        _myTransform.gameObject.layer = 12;
        isComplete = true;
    }
}