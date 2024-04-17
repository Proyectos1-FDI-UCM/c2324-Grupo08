using System.Collections;
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
    [SerializeField]
    float timeForFalling = 0.2f;
    public float walkingTime;
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
    public IEnumerator Tropiezo()
    {
        _rigidBody.velocity = Vector3.zero;
        float x = Random.Range(-45, 45);
        float minForceTropiezo = 0;
        if (_frankInput.falling) 
        {
            minForceTropiezo = 1f;
        }
        if (MaxTropiezoDist < minForceTropiezo)
        {
            Debug.LogError("Distancia demasiado baja");
        }
        if (walkingTime >= timeForFalling) 
        {
            _frankInput.enabled = false;
            walkingTime = 0;
        }
        float tropiezo = Random.Range(minForceTropiezo, MaxTropiezoDist);
        _rigidBody.velocity = (Quaternion.Euler(0f, 0f, x) * _directionVector).normalized;
        yield return new WaitForSeconds(tropiezo); 
        _frankInput.enabled = true;
        _rigidBody.velocity = Vector2.zero;
        _frankInput.falling = false;
    }
    public void RegisterX(float x)
    {
        _xvalue = x;
    }
    public void RegisterY(float y)
    {
        _yvalue = y;
    }
    [SerializeField]
    LayerMask interactuarLayer;
    public void interact()
    {
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
        if(walkingTime > timeForFalling)
        {
            _frankInput.falling = true;
        }

        _directionVector = new Vector3(_xvalue, _yvalue);

        if (!_frankInput.falling)
        {
            _rigidBody.velocity = _directionVector.normalized * _speedValue + new Vector3(atraccion.x, atraccion.y);
        }

        if (_directionVector != Vector3.zero)
        {
            if (!_frankInput.falling)
            {
                walkingTime += Time.deltaTime;
            }
            
            _lastMovementVector = _directionVector;
            _animator.SetFloat("MovimientoX", _xvalue);
            _animator.SetFloat("MovimientoY", _yvalue);
            _animator.SetBool("Rascadita", false);
            _animator.SetBool("Andando", true);
        }
        else
        {
            walkingTime = 0;
            _animator.SetBool("Andando", false);
            _animator.SetBool("Rascadita", true);
        }
       
    }
}