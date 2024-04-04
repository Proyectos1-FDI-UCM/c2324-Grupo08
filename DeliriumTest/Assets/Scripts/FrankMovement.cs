using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speedValue = 5f; 
    [SerializeField] private float _firstInertia;
    public float FirstInertia
    { get { return _firstInertia; } }
    [SerializeField] private float _secondInertia;
    public float SecondInertia
    {
        get { return _secondInertia; }
    }
    [SerializeField] private float _maxImpulse;
    private float _inertia;
    public float Inertia { get { return _inertia; } set { _inertia = value; } }
    
    #endregion
    #region references
    private InputManager _frankInput;
    private DashCompnent _dash;
    private Rigidbody2D _rigidBody;
    private static GameObject player;
    private Animator _animator;
    [SerializeField] private VomitComponent _vomitComponent;

    public static GameObject Player { get { return player; } }
    #endregion
    #region propiedades
    private float _xvalue;
    private float _yvalue;
    private Vector3 _directionVector;
    private Vector3 _atractionDirection;
    public Vector3 Direction { get { return _directionVector; } }
    private Vector3 _movementVector;
    public Vector3 _lastMovementVector;
    public Vector3 _vomitShootVector;
    #endregion
    // Start is called before the first frame update

    /// <param name="attractionToAdd"> speed to be add when collision </param>
    public void AddSpeed(Vector3 speedToAdd)
    {
        _atractionDirection = _speedValue * (_atractionDirection + speedToAdd).normalized;

    }
    public Animator GetAnimator()
    {
        _animator = GetComponent<Animator>();
        return _animator;
    }
    public void DashUpgrade()
    {
        _dash.DashUpgrade();
    }
    public void Dash()
    {
        _dash.Dash(_directionVector, _lastMovementVector);
    }
    public void RegisterX(float x)
    {
        _xvalue = x;
    }
    public void RegisterY(float y)
    {
        _yvalue = y;
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
        _frankInput = GetComponent<InputManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _dash = GetComponent<DashCompnent>();
        _lastMovementVector = Vector3.right;
    }
    public Vector2 atraccion;
    void FixedUpdate()
    {
        _directionVector = new Vector3(_xvalue, _yvalue);
        _movementVector = _directionVector.normalized * _speedValue + new Vector3(atraccion.x, atraccion.y); ;

        if (_frankInput.AddsInertia)
        {          
            _rigidBody.AddForce(_directionVector * _inertia);
            Vector2.ClampMagnitude(Vector2.zero, _maxImpulse);

        }
        else
        {
            _rigidBody.velocity = _movementVector;
        }
        if (_directionVector != Vector3.zero)
        {
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