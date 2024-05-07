using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour, EnemiesControler
{
    #region states
    [SerializeField] private ShootingState shootingState;
    [SerializeField] private PickingUpState pickingUpState;
    [SerializeField] private IdleState idleState;
    [SerializeField] private EscapingState escapingState;
    State state;
    #endregion

    #region properties
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    #endregion

    #region prameters
    // Tiempo de espera entre estados
    [SerializeField] private float escapingLenght = 10;
    [SerializeField] private float idleLenght = 1;

    public Vector3 directionMovement;
    public Vector3 positionBottle;
    public float currentSpeed;
    #endregion

    #region referneces
    [SerializeField] private BulletComponent bulletComp; //Compnente de bala (movimiento) de la botella

    private Animator animator;
    #endregion
    // Esta corrutina se llamará en el start y permite que pase d eun estado a otro además de que es recurisvo al estarse llamando así mismo todo el rato
    private IEnumerator SuccesionState()
    {
        state = escapingState;
        state.Enter();
        yield return new WaitForSeconds(escapingLenght);
        state = shootingState;
        state.Enter();
        yield return new WaitUntil(() => shootingState._bulletHit);
        state = pickingUpState;
        state.Enter();
        yield return new WaitUntil(() => !shootingState._bulletHit);
        state = idleState;
        state.Enter();
        yield return new WaitForSeconds(idleLenght);
        StartCoroutine(SuccesionState());

    }
    //Permite diferenciar al boss como enemigo
    public IEnumerator StopAttack()
    {
        yield return null;
    }

    private void Awake()
    {
       enabled = false;
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        MovementTropiezo frankDirection = FrankMovement.Player.GetComponent<MovementTropiezo>();
        enabled = gameObject.activeSelf;
        shootingState.SetUP(rb, bulletComp, frankDirection, boxCollider, this, transform, animator);
        pickingUpState.SetUP(rb, bulletComp, frankDirection, boxCollider, this, transform, animator);
        idleState.SetUP(rb, bulletComp, frankDirection, boxCollider, this, transform, animator);
        escapingState.SetUP(rb, bulletComp, frankDirection, boxCollider, this, transform, animator);
        state = escapingState;
        StartCoroutine(SuccesionState());
        animator.SetBool("EscapingState", true);
    }

    private void Update()
    {
        state.Do();
    }
}
