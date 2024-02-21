using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speedValue = 5f;
    [SerializeField] private float _dashforce = 5f;
    #endregion
    #region references
    private Rigidbody2D _rigiRigidbody;
    private static GameObject player;
    public static GameObject Player { get { return player; } }
    #endregion
    #region propiedades
    private float _xvalue;
    private float _yvalue;
    private Vector3 _directionVector;
    private Vector3 _movementVector;
    private Vector3 _lastMovementVector;
    #endregion
    // Start is called before the first frame update
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
        _rigiRigidbody = GetComponent<Rigidbody2D>();
        _lastMovementVector = Vector3.right;
    }

    public void RegisterX(float x)
    {
        _xvalue = x;
    }

    public void RegisterY(float y)
    {
        _yvalue = y;
    }

    public void Dash()
    {

        if (_directionVector != Vector3.zero)
        {
            transform.position = transform.position + (_dashforce * _directionVector);
        }
        else
        { transform.position = transform.position + (_dashforce * _lastMovementVector); }
        StartCoroutine(DashCoolDown());
    }


    void FixedUpdate()
    {
        _directionVector = new Vector3(_xvalue, _yvalue);
        _movementVector = _directionVector * _speedValue;
        _rigiRigidbody.velocity = _movementVector;
        if (_directionVector != Vector3.zero)
        {
            _lastMovementVector = _directionVector;
        }
    }
    // Update is called once per frame

    IEnumerator DashCoolDown()
    {
        _rigiRigidbody.velocity = Vector3.zero;
        this.enabled = false;
        yield return new WaitForSeconds(2f);
        this.enabled = true;
    }
}