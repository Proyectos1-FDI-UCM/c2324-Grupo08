using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    
    protected float startTime;
    
    protected Vector3 directionMovement;
    
    protected float speedValue;
    
    protected float currentSpeed;
    
    protected Rigidbody2D rb;
    
    protected Collider2D trigger;
    
    protected Vector3 positionBottle;
    
    protected GameObject botella;//prefab de la botella que lanza
    
    protected GameObject pickUp; //prefab del pickup de la botella
    
    protected GameObject _bullet; //Referencia a la botella lanzada
    
    protected BulletComponent bulletComp; //Compnente de bala (movimiento) de la botella
    
    protected FrankMovement frankDirection;

    public float time => Time.time - startTime;
    
    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedEnter() { }
    public virtual void Exit() { }

    public void SetUP(Rigidbody2D _rb, Collider2D _trigger, GameObject _botella, GameObject bullet, GameObject _pickUp, BulletComponent _bulletComp,
        FrankMovement _frankDirection) 
    {
        rb = _rb;
        trigger = _trigger;
        botella = _botella;
        bulletComp = _bulletComp;
        _bullet = bullet;
        frankDirection = _frankDirection;
        pickUp = _pickUp;

    }

}
