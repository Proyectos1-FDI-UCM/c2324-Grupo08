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

    public float time => Time.time - startTime;
    
    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedEnter() { }
    public virtual void Exit() { }
}
