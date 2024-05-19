using System.Collections;
using TMPro;
using UnityEngine;

//AVISO: Esta clase es una extensión de la clase 
//FrankMovement para implementar un movimiento 
//que de una mayor sensacion de borrachera.
//Esto se verá reflejado en el tropiezo y
//el caminar torcido de Paco.

public class MovementTropiezo : FrankMovement
{
    #region references
    
    /// <summary>
    /// Referencia a la extensión del inputManager 
    /// para implemetar el tropiezo.
    /// </summary>
    InputTropiezo _Input;
    #endregion

    #region propiedades
    
    /// <summary>
    /// Tiempo que Paco mantendra un angulo en una direccion.
    /// </summary>
    [SerializeField]
    float _timeForFalling = 0.2f;
    
    /// <summary>
    /// Tiempo que lleva Paco caminando.
    /// Este se reinicia cada _timeforfalling.
    /// </summary>
    public float walkingTime;

    /// <summary>
    /// Angulo maximo en el que puede desviarse Paco.
    /// Este rango va desde -(_drunkAngle) hasta _drunkAngle.
    /// </summary>
    [SerializeField]
    float _drunkAngle = 20f;

    /// <summary>
    /// Angulo en el que paco se esta desviando de la dirección 
    /// marcada por el input.
    /// </summary>
    float _trueDrunkAngle;
    #endregion

    #region Coroutines
    /// <summary>
    /// Corutina que sobre escribe a la que contiene el FrankMovement.
    /// Paco es empujado con una pequeña fuerza e inhabilita por 0.5 seg
    /// el inputTropiezo. Esto hace que no se pueda mover.
    /// </summary>
    /// <returns>Espera 0.5 segundos antes de volver a activar el input</returns>
    public new IEnumerator Tropiezo()
    {

        _xvalue = 0;
        _yvalue = 0;
        _rigidBody.velocity = Vector3.zero;
        float x = Random.Range(-45, 45);
        float minForceTropiezo = 0;
        if (_Input.falling)
        {
            minForceTropiezo = MaxTropiezoDist / 8;
        }
        walkingTime = 0;
        float tropiezo = Random.Range(minForceTropiezo, MaxTropiezoDist);
        _rigidBody.velocity = (Quaternion.Euler(0f, 0f, x) * _lastMovementVector).normalized;
        if (tropiezo > MaxTropiezoDist * 0.8f)
        {
            _animator.SetBool("Rascadita", true);
            _animator.SetBool("Tropiezo", true);
            _Input.enabled = false;
            yield return new WaitForSecondsRealtime(0.5f);
            _Input.enabled = true;
            _Input.falling = false;
            _animator.SetBool("Tropiezo", false);

        }
    }
    #endregion
    private void Awake()
    {
        //Singleton de Paco
        if (player == null)
        {
            player = gameObject;
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        _Input = GetComponent<InputTropiezo>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _dash = GetComponent<DashCompnent>();
        _lastMovementVector = Vector3.right;
    }
    void FixedUpdate()
    {
        //Cambio de angulo en el movimiento cuando lleves mas
        //tiempo andando que el _timeForFalling.
        if (walkingTime > _timeForFalling)
        {
            walkingTime = 0;
            _trueDrunkAngle = Random.Range(-_drunkAngle, _drunkAngle);
        }

        //Cogemos la direccion indicada por el inputTropiezo.
        _directionVector = new Vector3(_xvalue, _yvalue);

        //Movemos a Paco en función de su angulo.
        _rigidBody.velocity = Quaternion.Euler(0f, 0f, _trueDrunkAngle) * _directionVector.normalized * _speedValue + new Vector3(atraccion.x, atraccion.y);

        //Animacion
        if (_directionVector != Vector3.zero)
        {
            walkingTime += Time.deltaTime;
            _lastMovementVector = _directionVector;
            _animator.SetFloat("MovimientoX", _xvalue);
            _animator.SetFloat("MovimientoY", _yvalue);
            _animator.SetBool("Rascadita", false);
            _animator.SetBool("Andando", true);
        }
        else
        {
            _animator.SetBool("Andando", false);
            _animator.SetBool("Rascadita", true);
        }

    }
}