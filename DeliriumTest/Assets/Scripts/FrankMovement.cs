using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speedValue = 5f;
    [SerializeField] private float _dashforce = 5f;
    [SerializeField] private float _boundOffset;    
    [SerializeField] private float _firstInertia;
    public float FirstInertia
    { get { return _firstInertia; } }
    [SerializeField] private float _secondInertia;
    public float SecondInertia
    {
        get { return _secondInertia; }
    }
    [SerializeField] private float _maxImpulse;
    [SerializeField] private float _DashStun = 0.5f;
    [SerializeField] private float _DashCooldown = 1.0f;
    [SerializeField] private float _NewDashCooldown = 0.5f;
    private float _elapsedTime;
    private float _inertia;
    public float Inertia { get { return _inertia; } set { _inertia = value; } }
    
   
    #endregion
    #region references
    private InputManager _frankInput;
    private Rigidbody2D _rigiRigidbody;
    private static GameObject player;
    private Animator _animator;
    [SerializeField] private VomitComponent _vomitComponent;
    [SerializeField] private CameraController _camController;
    [SerializeField] private Transform _UpperBound;
    [SerializeField] private Transform _LowerBound;


    public static GameObject Player { get { return player; } }
    #endregion
    #region propiedades
    private float _xvalue;
    private float _yvalue;
    private Vector3 _directionVector;
    public Vector3 Direction { get { return _directionVector; } }
    private Vector3 _movementVector;
    public Vector3 _lastMovementVector;
    [SerializeField] private RigidbodyConstraints2D _originalConstraints;
    private Vector3 _dashPosition;
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
        _animator = GetComponent<Animator>();
        _frankInput = GetComponent<InputManager>();
        _rigiRigidbody = GetComponent<Rigidbody2D>();
        _lastMovementVector = Vector3.right;
        _elapsedTime = 0;
    }

    
    public void DashUpgrade()
    {
        _DashCooldown = _NewDashCooldown;
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
        Debug.Log("Llama");
        if (_elapsedTime >= _DashCooldown)
        {
            if (_directionVector != Vector3.zero)
            {
                _dashPosition = transform.position + (_dashforce * _directionVector.normalized);
            }
            else
            {
                _dashPosition = transform.position + (_dashforce * _lastMovementVector.normalized);
            }
            if ((_dashPosition.x > _camController.leftCamBound + _boundOffset) && (_dashPosition.x < _camController.rightCamBound - _boundOffset) && (_dashPosition.y < _UpperBound.position.y - _boundOffset) && (_dashPosition.y > _LowerBound.position.y + _boundOffset))
            {
                transform.position = _dashPosition;
                _vomitComponent.VomitDash();
                StartCoroutine(DashCoolDown());
                _elapsedTime = 0;
            }
        }
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
            _animator.SetFloat("MovimientoY", _yvalue) ;
            _animator.SetBool("Rascadita", false);
            _animator.SetBool("Andando", true);
        }
        else
        {
            _animator.SetBool("Andando", false);
            _animator.SetBool("Rascadita", true);
        }
        
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }
    // Update is called once per frame

    IEnumerator DashCoolDown()
    {
        _rigiRigidbody.velocity = Vector3.zero;
        _frankInput.enabled = false;
        _rigiRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        //this.enabled = false;
        yield return new WaitForSeconds(_DashStun);
        _rigiRigidbody.constraints = _originalConstraints;
        _frankInput.enabled = true;
        //this.enabled = true;
    }
}