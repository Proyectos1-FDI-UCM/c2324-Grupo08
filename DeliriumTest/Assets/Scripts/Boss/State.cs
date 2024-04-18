using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected Rigidbody2D rb;

    protected CircleCollider2D trigger;

    protected BulletComponent bulletComp; //Componente de bala (movimiento) de la botella

    protected FrankMovement frankDirection;

    protected BossController bossMovement;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedEnter() { }
    public virtual void Exit() { }

    public void SetUP(Rigidbody2D _rb, CircleCollider2D _trigger, BulletComponent _bulletComp, FrankMovement _frankDirection,
        BossController _bossMovement)
    {
        rb = _rb;
        trigger = _trigger;
        frankDirection = _frankDirection;
        bossMovement = _bossMovement;
    }

}
