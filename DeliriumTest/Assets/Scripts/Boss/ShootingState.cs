using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingState : State
{
    //Estado en el que lanza la botella cierta distancia hacia el jugador

    /// <summary>
    /// prefab de la botella que lanza
    /// </summary>
    public GameObject botella;

    /// <summary>
    /// prefab del pickup de la botella
    /// </summary>
    public GameObject pickUp;

    /// <summary>
    /// Referencia a la botella lanzada
    /// </summary>
    public GameObject _bullet;

    /// <summary>
    /// Indicador para saber cuando golpea la bala algun objeto
    /// </summary>
    public bool _bulletHit;

    /// <summary>
    /// Se instancia una botellla que va en dirección al jugador.
    /// Mientras la botella del boss no golpee nada el boss será invencible y estara quieto.
    /// </summary>
    public override void Enter()
    {
        _animation.SetBool("AttackState", true);
        _animation.SetBool("EscapingState", false);
        _animation.SetBool("IdleState", false);
        _animation.SetBool("DashState", false);
        _bulletHit = false;
        isComplete = false;
        rb.velocity = Vector3.zero;
        _bullet = Instantiate(botella, transform.position, Quaternion.identity);
        _bullet.GetComponent<BossBullet>().shootingState = this;
        bulletComp = _bullet.GetComponent<BulletComponent>();
        bulletComp.RegisterVector(FrankMovement.Player.transform.position - transform.position);
        _myTransform.gameObject.layer = 10;
    }
    public override void Do()
    {
        rb.velocity = Vector3.zero;
        isComplete = true;
    }
}
