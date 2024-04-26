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
    public override void Enter()
    {
        _bulletHit = false;
        isComplete = false;
        _bullet = Instantiate(botella, transform.position, Quaternion.identity);
        _bullet.GetComponent<BossBullet>().shootingState = this;
        bulletComp = _bullet.GetComponent<BulletComponent>();
        bulletComp.RegisterVector(FrankMovement.Player.transform.position - transform.position);
    }
    public override void Do()
    {
        isComplete = true;
    }
}
