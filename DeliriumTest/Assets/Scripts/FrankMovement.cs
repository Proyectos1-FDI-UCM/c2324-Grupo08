using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speedValue = 5f;
    [SerializeField] private float _dashforce = 5f;
    [SerializeField] private float _boundOffset;

    #endregion
    #region references
    private InputManager _frankInput;
    private Rigidbody2D _rigiRigidbody;
    private static GameObject player;
    [SerializeField] private VomitComponent _vomitComponent;
    [SerializeField] private Transform _LeftBound;
    [SerializeField] private Transform _RightBound;
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
        _frankInput = GetComponent<InputManager>();
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
            _dashPosition = transform.position + (_dashforce * _directionVector.normalized);
        }
        else
        {
            _dashPosition = transform.position + (_dashforce * _lastMovementVector.normalized);
        }
        if ((_dashPosition.x > _LeftBound.position.x + _boundOffset) && (_dashPosition.x < _RightBound.position.x - _boundOffset) && (_dashPosition.y < _UpperBound.position.y - _boundOffset) && (_dashPosition.y > _LowerBound.position.y + _boundOffset))
        {
            transform.position = _dashPosition;
            _vomitComponent.VomitDash();
            StartCoroutine(DashCoolDown());
        }



    }


    void FixedUpdate()
    {
        _directionVector = new Vector3(_xvalue, _yvalue);
        _movementVector = _directionVector.normalized * _speedValue;
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
        _frankInput.enabled = false;
        _rigiRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        //this.enabled = false;
        yield return new WaitForSeconds(1f);
        _rigiRigidbody.constraints = _originalConstraints;
        _frankInput.enabled = true;
        //this.enabled = true;
    }
}