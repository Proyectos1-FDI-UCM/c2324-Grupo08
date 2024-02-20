using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speedValue = 5f;
    [SerializeField] private float _dashforce = 5f;
    #endregion

    #region references
    private Rigidbody2D _rigiRigidbody;
    private InputManager _frankInput;
    [SerializeField]
    private static GameObject player;
    public static GameObject Player { get { return player; } }
    #endregion

    #region propiedades
    private float _xvalue;
    private float _yvalue;
    private Vector3 _directionVector;
    private Vector3 _movementVector;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _rigiRigidbody = GetComponent<Rigidbody2D>();
        _frankInput = GetComponent<InputManager>();
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
        transform.position = transform.position + (_dashforce * _directionVector);
    }

    private void Awake()
    {
        if(player == null) player = this.gameObject;
        else Destroy(gameObject);
    }

    void FixedUpdate()
    {
        _directionVector = new Vector3(_xvalue, _yvalue);
        _movementVector = _directionVector * _speedValue;
        _rigiRigidbody.velocity = _movementVector;
    }
    
}
