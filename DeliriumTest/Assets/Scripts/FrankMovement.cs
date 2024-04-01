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
    private Rigidbody2D _rigiRigidbody;
    private static GameObject player;
    private Animator _animator;
    [SerializeField] private VomitComponent _vomitComponent;

    public static GameObject Player { get { return player; } }
    #endregion
    #region propiedades
    private float _xvalue;
    private float _yvalue;
    private Vector3 _directionVector;
    public Vector3 Direction { get { return _directionVector; } }
    private Vector3 _movementVector;
    public Vector3 _lastMovementVector;
    public Vector3 _vomitShootVector;
    #endregion
    // Start is called before the first frame update
    public Animator GetAnimator()
    {
        _animator = GetComponent<Animator>();
        return _animator;
    }
    public void DashUpgrade()
    {
        _dash.DashUpgrade();
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
        _dash.Dash(_directionVector, _lastMovementVector);
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
        _rigiRigidbody = GetComponent<Rigidbody2D>();
        _dash = GetComponent<DashCompnent>();
        _lastMovementVector = Vector3.right;
    }
    void FixedUpdate()
    {
        _directionVector = new Vector3(_xvalue, _yvalue);
        _movementVector = _directionVector.normalized * _speedValue;

        /*if (_frankInput.AddsInertia)
        {          
            
            
            _rigiRigidbody.AddForce(_directionVector * _inertia);
            _rigiRigidbody.velocity = Vector2.ClampMagnitude(_rigiRigidbody.velocity, _maxImpulse);
        }

        else
        {*/
        _rigiRigidbody.velocity = _movementVector;
        //}



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