using System.Collections;
using UnityEngine;

public class MovementTropiezo : FrankMovement
{
    #region references
    InputTropiezo _Input;
    #endregion
    #region propiedades
    [SerializeField]
    float _timeForFalling = 0.2f;
    public float walkingTime;
    [SerializeField]
    float _drunkAngle = 20f;
    float _trueDrunkAngle;
    #endregion

    public new IEnumerator Tropiezo()
    {

        _xvalue = 0;
        _yvalue = 0;
        _rigidBody.velocity = Vector3.zero;
        float x = Random.Range(-45, 45);
        float minForceTropiezo = 0;
        if (_Input.falling)
        {
            minForceTropiezo = MaxTropiezoDist / 5;
        }
        walkingTime = 0;
        float tropiezo = Random.Range(minForceTropiezo, MaxTropiezoDist);
        _rigidBody.velocity = (Quaternion.Euler(0f, 0f, x) * _lastMovementVector).normalized;
        if (tropiezo > MaxTropiezoDist/1.2)
        {
            _animator.SetBool("Rascadita", true);
            _animator.SetBool("Tropiezo", true);
            _Input.enabled = false;
            yield return new WaitForSecondsRealtime(1f);
            _Input.enabled = true;
            _Input.falling = false;
            _animator.SetBool("Tropiezo", false);

        }
    }
    private void Awake()
    {
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

        if (walkingTime > _timeForFalling)
        {
            walkingTime = 0;
            _trueDrunkAngle = Random.Range(-_drunkAngle, _drunkAngle);
        }

        _directionVector = new Vector3(_xvalue, _yvalue);

        _rigidBody.velocity = Quaternion.Euler(0f, 0f, _trueDrunkAngle) * _directionVector.normalized * _speedValue + new Vector3(atraccion.x, atraccion.y);

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