using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speedValue = 5f;
    [SerializeField] private float _dashforce = 5f;
    #endregion
    #region references
    private Rigidbody2D _rigiRigidbody;
    private FrankMovement _frankInput;
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
        _frankInput = GetComponent<FrankMovement>();
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

    
    void FixedUpdate()
    {
        _directionVector = new Vector3(_xvalue, _yvalue);
        _movementVector = _directionVector * _speedValue;
        _rigiRigidbody.velocity = _movementVector;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
