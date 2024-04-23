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
    #endregion

    #region properties
    [SerializeField] private Rigidbody2D rb;
    private Transform transform;
    #endregion

    #region referneces
    [SerializeField] private BulletComponent bulletComp; //Compnente de bala (movimiento) de la botella
    [SerializeField] private FrankMovement frankDirection;
    [SerializeField] private Transform player;
    #endregion
    private IEnumerator SuccesionState()
    {
        state = escapingState;
        yield return new WaitForSeconds(1f);
        state = shootingState;
        yield return new WaitForSeconds(1f);
        state = pickingUpState;
        yield return new WaitForSeconds(1f);
        state = idleState;
        timer = startTimer;
    }
    private void SelectState()
    {
        StartCoroutine(SuccesionState());
        state.Enter();
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
        shootingState.SetUP(rb, bulletComp, frankDirection, this,transform);
        pickingUpState.SetUP(rb, bulletComp, frankDirection, this,transform);
        idleState.SetUP(rb, bulletComp, frankDirection, this,transform);
        escapingState.SetUP(rb, bulletComp, frankDirection, this, transform);
    }
    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        state = escapingState;
        startTimer = timer;
    }
    private void Update()
    {
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);
        state.Do();
    }
}
