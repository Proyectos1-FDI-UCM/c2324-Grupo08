using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    // Clase abstracta de la cual van a heredar el resta de estados, al ser MonoBehaviour los estados que proengan de esta clase podrán relaizar todas las operaciones y 
    // funciones de MonoBehaviour. Debido a que varios estados deben de usar el RigidBody o el ANimator se protegen en esta clase y así pueden ser accesibles entre
    // los distintos estados.
    public bool isComplete { get; protected set; }

    protected Rigidbody2D rb;

    protected BoxCollider2D boxCollider;

    protected BulletComponent bulletComp; //Componente de bala (movimiento) de la botella

    protected FrankMovement frankDirection;

    protected BossController bossMovement;
    
    protected Transform _myTransform;

    protected Animator _animation;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedEnter() { }
    public virtual void Exit() { }
    
    //Para que pueda recoger la información de las distintas propiedades y/o referencias es necesario hacer un método constructor par asignar esos valores
    public void SetUP(Rigidbody2D _rb, BulletComponent _bulletComp, FrankMovement _frankDirection, BoxCollider2D _boxCollider,
        BossController _bossMovement, Transform myTransform, Animator animator)
    {
        rb = _rb;
        frankDirection = _frankDirection;
        bossMovement = _bossMovement;
        _myTransform = myTransform;
        boxCollider = _boxCollider;
        _animation = animator;
    }

}
