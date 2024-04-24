using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingState : State
{//Estado en el que lanza la botella cierta distancia hacia el jugador
    [SerializeField] private float distance;
    public GameObject botella;//prefab de la botella que lanza
    public GameObject pickUp; //prefab del pickup de la botella
    public GameObject _bullet; //Referencia a la botella lanzada
    public override void Enter()
    {
        _bullet = Instantiate(botella, transform.position, Quaternion.identity);
        bulletComp = _bullet.GetComponent<BulletComponent>();
        bulletComp.RegisterVector(FrankMovement.Player.transform.position - transform.position);
    }
    public override void Do()
    {
        Vector3 vectorDist = _bullet.transform.position - transform.position;
        if (vectorDist.magnitude >= distance)
        {
            GameObject bulletInstance = Instantiate(pickUp, _bullet.transform.position, Quaternion.identity);
            bossMovement.positionBottle = _bullet.transform.position;
            Destroy(bulletInstance);
            isComplete = true;
        }
    }
}
