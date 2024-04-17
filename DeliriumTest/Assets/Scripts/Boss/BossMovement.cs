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

    #region states
    [SerializeField] private ShootingState shootingState;
    [SerializeField] private PickingUpState pickingUpState;
    [SerializeField] private IdleState idleState;
    [SerializeField] private EscapingState escapingState;
    State state;
    #endregion

    #region properties
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D trigger;
    #endregion

    #region referneces
    [SerializeField] private BulletComponent bulletComp; //Compnente de bala (movimiento) de la botella
    [SerializeField] private FrankMovement frankDirection;
    [SerializeField] private Transform player;
    #endregion
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
        shootingState.SetUP(rb, trigger, bulletComp, frankDirection, this);
        pickingUpState.SetUP(rb, trigger, bulletComp, frankDirection, this);
        idleState.SetUP(rb, trigger, bulletComp, frankDirection, this);
        escapingState.SetUP(rb, trigger, bulletComp, frankDirection, this);
    }
    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        trigger = GetComponent<Collider2D>();       
        state = escapingState;
        state.Enter();
    }
    private void Update()
    {
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);       
        state.Do();
    }
}
