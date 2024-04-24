using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private bool isFacingRIght = true;
    public Vector3 directionMovement;
    public Vector3 positionBottle;
    public float currentSpeed;
    [SerializeField] private float timer;
    private float startTimer;

    #region states
    [SerializeField] private ShootingState shootingState;
    [SerializeField] private PickingUpState pickingUpState;
    [SerializeField] private IdleState idleState;
    [SerializeField] private EscapingState escapingState;
    State state;
    State firstState;
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
    #endregion
    private IEnumerator SuccesionState()
    {
        Debug.Log("Ciclo");
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
        state = firstState;
        yield return new WaitForSeconds(escapingLenght);
        state.Enter();
        StartCoroutine(SuccesionState());
        
    }
    //Esto hace que el enemigo mire en la dirección en la que está el jugador
    private void Flip(bool isPlayerRight)
    {
        //Comprueba el lado al que mira y si el jugador se mueve a la izda. se gira a esa dirección
        if (isFacingRIght && !isPlayerRight || !isFacingRIght && isPlayerRight)
        {
            isFacingRIght = !isFacingRIght;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Awake()
    {

        bossTransform = GetComponent<Transform>(); 
        shootingState.SetUP(rb, bulletComp, frankDirection,boxCollider, this,transform);
        pickingUpState.SetUP(rb, bulletComp, frankDirection,boxCollider, this,transform);
        idleState.SetUP(rb, bulletComp, frankDirection, boxCollider, this,transform);
        escapingState.SetUP(rb, bulletComp, frankDirection,boxCollider, this, transform);
    }
    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        state = escapingState;
        firstState = state;
        startTimer = timer;
        StartCoroutine(SuccesionState());
    }
    private void Update()
    {
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);
        state.Do();
    }
}
