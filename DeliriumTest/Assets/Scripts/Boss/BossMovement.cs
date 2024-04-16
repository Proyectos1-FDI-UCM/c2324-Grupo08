using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{    
    private bool isFacingRIght = true;
    private int stateCounter;
    [SerializeField] private float timeState;

    #region states
    private ShootingState shootingState;   
    private PickingUpState pickingUpState;   
    private IdleState idleState;   
    private EscapingState escpaingState;
    private State state;
    #endregion

    #region properties
    private Rigidbody2D rb;
    private Collider2D trigger;
    private GameObject botella;//prefab de la botella que lanza
    private GameObject pickUp; //prefab del pickup de la botella
    private GameObject _bullet; //Referencia a la botella lanzada
    #endregion

    #region referneces
    [SerializeField] private BulletComponent bulletComp; //Compnente de bala (movimiento) de la botella
    [SerializeField] private FrankMovement frankDirection;
    [SerializeField] private Transform player;
    #endregion
    private void SelectState()
    {
        if(state.isComplete)
        {
            if (stateCounter == 0)
            {
                state = escpaingState;
            }
            else if (stateCounter == 1)
            {
                state = shootingState;
            }
            else if (stateCounter == 2)
            {
                state = pickingUpState;
            }
            else if (stateCounter == 3)
            {
                state = idleState;
            }
        }
        state.Enter();
    }
    private IEnumerator StateDoer()
    {
        if (stateCounter == 3)
        {
            stateCounter = 0;
        }
        SelectState();
        state.Do();
        yield return new WaitForSeconds(timeState);
        stateCounter++;

    }
    //Esto hace que el enemigo mire en la dirección en la que está el jugador
    private void Flip(bool isPlayerRight)
    {
        //Comprueba el lado al que mira y si el jugador se mueve a la izda. se gira a esa dirección
        if(isFacingRIght && !isPlayerRight|| !isFacingRIght && isPlayerRight) 
        {
            isFacingRIght = !isFacingRIght;
            Vector3 scale =transform.localScale;
            scale.x *= -1;
            transform.localScale =scale; 
        }
    }
    private void Start()
    {
        shootingState.SetUP(rb, trigger, botella, _bullet, pickUp, bulletComp, frankDirection);
        pickingUpState.SetUP(rb, trigger, botella, _bullet, pickUp, bulletComp, frankDirection);
        idleState.SetUP(rb, trigger, botella, _bullet, pickUp, bulletComp, frankDirection);
        escpaingState.SetUP(rb, trigger, botella, _bullet, pickUp, bulletComp, frankDirection);
        
        stateCounter = 0;
    }
    private void Update()
    {
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);
        StateDoer();
    }
}
