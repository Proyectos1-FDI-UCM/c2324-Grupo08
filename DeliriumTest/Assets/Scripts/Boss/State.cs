using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected Rigidbody2D rb;

    protected BoxCollider2D boxCollider;

    protected BulletComponent bulletComp; //Componente de bala (movimiento) de la botella

    protected FrankMovement frankDirection;

    protected BossController bossMovement;
    
    protected Transform _myTransform;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedEnter() { }
    public virtual void Exit() { }

    public void SetUP(Rigidbody2D _rb, BulletComponent _bulletComp, FrankMovement _frankDirection, BoxCollider2D _boxCollider,
        BossController _bossMovement, Transform myTransform)
    {
        rb = _rb;
        frankDirection = _frankDirection;
        bossMovement = _bossMovement;
        _myTransform = myTransform;
        boxCollider = _boxCollider;
    }

}
