using UnityEngine;

public class FrankMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speedValue = 5f;
    [SerializeField] private int MaxTropiezoDist;
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
    public Vector2 atraccion;
    private Vector3 _directionVector;
    public Vector3 Direction { get { return _directionVector; } }
    public Vector3 _lastMovementVector;
    [SerializeField] LayerMask interactuarLayer;

    #endregion
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
    public void Tropiezo()
    {

        if ((new Vector3(_xvalue, _yvalue, 0)).Equals(Vector3.zero))
        {
            Vector3 _cameraPosition = Camera.main.transform.position;
            float x = Random.Range(-45, 45);
            int tropiezo = Random.Range(0, MaxTropiezoDist + 1);
            Vector3 _tropiezoVect = (Quaternion.Euler(0f, 0f, x) * (_directionVector)).normalized * tropiezo;
            _tropiezoVect += transform.position;
            if (_tropiezoVect.x > (_cameraPosition - Vector3.right * 8).x && _tropiezoVect.x < (_cameraPosition + Vector3.right * 8).x && _tropiezoVect.y > (_cameraPosition - Vector3.up * 5).y && _tropiezoVect.y < (_cameraPosition + Vector3.up * 3).y)
            {
                transform.position = _tropiezoVect;
                //if (tropiezo != 0) animación
                //Si quieres meterle un yieldreturn de tiempo hazlo corrutina, deberia funcionar igual, aunque lo mismo hay que desactivar Input.
            }
        }
    }
    public void RegisterX(float x)
    {
        _xvalue = x;
    }
    public void RegisterY(float y)
    {
        _yvalue = y;
    }
    public void interact()
    {
        Debug.Log("Pulso interacción");
        var interactposition = transform.position;

        var collider = Physics2D.OverlapCircle(interactposition, 1f, interactuarLayer);
        if (collider != null)
        {
            collider.GetComponent<interactables>()?.interact();

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
        _frankInput = GetComponent<InputManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _dash = GetComponent<DashCompnent>();
        _lastMovementVector = Vector3.right;
    }
    void FixedUpdate()
    {
        _directionVector = new Vector3(_xvalue, _yvalue);
        _rigidBody.velocity = _directionVector.normalized * _speedValue + new Vector3(atraccion.x, atraccion.y);
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