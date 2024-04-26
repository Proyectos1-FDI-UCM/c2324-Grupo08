using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, EnemiesControler
{
    private bool isFacingRIght = true;
    public Vector3 directionMovement;
    public Vector3 positionBottle;
    public float currentSpeed;
    [SerializeField] private float timer;

    #region states
    [SerializeField] private ShootingState shootingState;
    [SerializeField] private PickingUpState pickingUpState;
    [SerializeField] private IdleState idleState;
    [SerializeField] private EscapingState escapingState;
    State state;
    #endregion

    #region properties
    [SerializeField] private Rigidbody2D rb;
    private Transform bossTransform;
    private BoxCollider2D boxCollider;
    [SerializeField] private float escapingLenght = 10;
    [SerializeField] private float shootingLenght = 1;
    [SerializeField] private float pickingLenght = 1;
    [SerializeField] private float idleLenght = 1;
    #endregion

    #region referneces
    [SerializeField] private BulletComponent bulletComp; //Compnente de bala (movimiento) de la botella
    [SerializeField] private FrankMovement frankDirection;
    [SerializeField] private Transform player;
    private Animator animator;
    #endregion
    private IEnumerator SuccesionState()
    {
        state = escapingState;
        state.Enter();
        yield return new WaitForSeconds(escapingLenght);
        state = shootingState;
        state.Enter();
        if(shootingState._bullet != null)
        {
            yield return new WaitForSeconds(shootingLenght);
            state = pickingUpState;
            state.Enter();
            yield return new WaitForSeconds(pickingLenght);      
        }        
        state = idleState;
        yield return new WaitForSeconds(idleLenght);
        state.Enter();
        yield return new WaitForSeconds(1f);
        StartCoroutine(SuccesionState());
        
    }
    public IEnumerator StopAttack()
    {
        yield return null;
    }

    private void Awake()
    {

        bossTransform = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        shootingState.SetUP(rb, bulletComp, frankDirection,boxCollider, this,transform,animator);
        pickingUpState.SetUP(rb, bulletComp, frankDirection,boxCollider, this,transform, animator);
        idleState.SetUP(rb, bulletComp, frankDirection, boxCollider, this,transform, animator);
        escapingState.SetUP(rb, bulletComp, frankDirection,boxCollider, this, transform, animator);
    }
    private void Start()
    {   
        state = escapingState;
        StartCoroutine(SuccesionState());
        animator.SetBool("EscapingState", true);
    }
    private void Update()
    {
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        state.Do();
    }
}
