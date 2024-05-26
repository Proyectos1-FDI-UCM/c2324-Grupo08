using System.Collections;
using UnityEngine;

public class DashCompnent : MonoBehaviour
{
    #region references
    private Rigidbody2D _rigiRigidbody;
    private FrankMovement _frankMovement;
    private InputManager _frankInput;
    [SerializeField] private VomitComponent _vomitComponent;
    [SerializeField] private GameObject _dashIndicator;
    #endregion
    #region parameters
    [SerializeField] private float _dashforce = 5f;
    [SerializeField] private float _DashStun = 0.5f;
    [SerializeField] private float _DashCooldown = 1.0f;
    [SerializeField] private float _NewDashCooldown = 0.5f;
    private float _elapsedTime;
    private Animator _animator;

    #endregion
    #region properties
    public bool dashMejorado;
    private Vector3 _dashPosition;
    private Vector3 _cameraPosition;
    #endregion
    /// <summary>
    /// Al llamar a este método por adquirir la mejora, se cambia el cooldown a uno más bajo. 
    /// dashMejorado se pone a true para mostrar el icono en la UI
    /// </summary>
    public void DashUpgrade()
    {
        _DashCooldown = _NewDashCooldown;
        dashMejorado = true;
    }
    /// <summary>
    /// Calcula la posicón del dash tomando en cuenta la última o actual dirección. 
    /// Si no se sale de los bordes de la cámara, cambia la posición del jugador a la nueva. 
    /// Ejecuta la corrutina de stun, y aumenta el medidor del vómito. 
    /// Vuelve a iniciar el contador del cooldown y desactiva el indicador de dash.
    /// </summary>
    /// <param name="_directionVector"></param>
    /// <param name="_lastMovementVector"></param>
    public void Dash(Vector3 _directionVector, Vector3 _lastMovementVector)
    {
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
            if (_dashPosition.x > (_cameraPosition - Vector3.right * 8).x && _dashPosition.x < (_cameraPosition + Vector3.right * 8).x && _dashPosition.y > (_cameraPosition - Vector3.up * 5).y && _dashPosition.y < (_cameraPosition + Vector3.up * 3).y)
            {
                transform.position = _dashPosition;
                if (_vomitComponent != null)
                {
                    _vomitComponent.VomitDash();
                }
                StartCoroutine(DashCoolDown());
                _elapsedTime = 0;
                if (_dashIndicator != null) 
                { 
                    _dashIndicator.SetActive(false); 
                }
            }
        }
    }

    /// <summary>
    /// Ejecuta el audio y animación de Dash, y desactiva el movimiento del jugador con un stun
    /// </summary>
    IEnumerator DashCoolDown()
    {
        // Se reproduce el efecto de sonido
        AudioManager.Instance.Dash();
        _animator.SetBool("Dash", true);
        _rigiRigidbody.velocity = Vector3.zero;
        _frankMovement.enabled = false;
        _frankInput.enabled = false;
        yield return new WaitForSeconds(_DashStun);
        _animator.SetBool("Dash", false);
        _frankInput.enabled = true;
        _frankMovement.enabled = true;
    }
    /// <summary>
    /// Cuando se desactive el componente, se desactiva el icono
    /// </summary>
    private void OnDisable()
    {
        if (_dashIndicator != null) _dashIndicator.SetActive(false);
    }
    /// <summary>
    /// Cuando se activa el componente, se activa el icono
    /// </summary>
    private void OnEnable()
    {
        if (_dashIndicator != null) _dashIndicator.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        dashMejorado = false;
        _animator = GetComponent<Animator>();
        _elapsedTime = _DashCooldown;
        _rigiRigidbody = GetComponent<Rigidbody2D>();
        _frankInput = GetComponent<InputManager>();
        _frankMovement = GetComponent<FrankMovement>();
        if(_dashIndicator == null)
        {
            Debug.Log("Falta la asignacion del Indicador del Dash.");
        }
        if (_vomitComponent == null)
        {
            Debug.Log("Falta la asignacion del Componente \"VomitComponent\".");
        }
    }

    // Update is called once per frame
    /// <summary>
    /// Actualiza _cameraPosition con la posición de la cámara, aumenta el contador de cooldown
    /// Si el contador supera el cooldown activa el indicador.
    /// </summary>
    void Update()
    {
        _cameraPosition = Camera.main.transform.position;
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _DashCooldown && _dashIndicator != null) _dashIndicator.SetActive(true);
    }
}
