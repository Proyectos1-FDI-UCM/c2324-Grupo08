using System.Collections;
using UnityEngine;

public class DashCompnent : MonoBehaviour
{
    #region references
    private Rigidbody2D _rigiRigidbody;
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
    public void DashUpgrade()
    {
        _DashCooldown = _NewDashCooldown;
        dashMejorado = true;

    }
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
                _vomitComponent.VomitDash();
                StartCoroutine(DashCoolDown());
                _elapsedTime = 0;
                _dashIndicator.SetActive(false);
            }
        }
    }
    IEnumerator DashCoolDown()
    {
        _animator.SetBool("Dash", true);
        _rigiRigidbody.velocity = Vector3.zero;
        _frankInput.enabled = false;
        yield return new WaitForSeconds(_DashStun);
        _animator.SetBool("Dash", false);
        _frankInput.enabled = true;
    }
    private void OnDisable()
    {
        if (_dashIndicator != null) _dashIndicator.SetActive(false);
    }
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
    }

    // Update is called once per frame
    void Update()
    {
        _cameraPosition = Camera.main.transform.position;
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _DashCooldown) _dashIndicator.SetActive(true);
    }
}
