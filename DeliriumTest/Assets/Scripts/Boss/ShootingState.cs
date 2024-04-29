using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingState : State
{//Estado en el que lanza la botella cierta distancia hacia el jugador
    public GameObject botella;//prefab de la botella que lanza
    public GameObject pickUp; //prefab del pickup de la botella
    public GameObject _bullet; //Referencia a la botella lanzada
    public bool _bulletHit;
    //Se instancia un abotellla que va en dirección al jugador
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
