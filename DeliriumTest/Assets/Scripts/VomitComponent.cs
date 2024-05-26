using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VomitComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _timerspeed;
    private float timer;
    [SerializeField] private float _maxvomit;
    [SerializeField] private float _vomitDash;
    public float _vomitcuantity;
    #endregion
    #region references
    private Slider _vomitBar;
    [SerializeField] private FrankMovement _frankMovement;
    [SerializeField] private InputManager _frankInput;
    [SerializeField] private Rigidbody2D _targetRigidBody;
    [SerializeField] private ShootComponent _shootComponent;
    private bool _stops = false;
    private Animator _animator;
    #endregion
    #region propiedades
    [SerializeField] private float vomitReduce;
    #endregion

    // Start is called before the first frame update

    /// <summary>
    /// Se obtienen las referencias al "Animator" y al "Slider" que muestra la cantidad de v�mito
    /// Se inicia la cantidad de v�mito a 0
    /// </summary>
    void Start()
    {
        _animator = _frankMovement.GetAnimator();
        
        _vomitBar = GetComponent<Slider>();
        if (_vomitBar != null ) 
        {
            _vomitBar.value = 0;
        }
        else
        {
            Debug.Log("Falta la asignacion del Slider del Vomito");
            _vomitBar = new GameObject().AddComponent<Slider>();
        }
    }

    /// <summary>
    /// Se a�ade una cantidad determinada de v�mito al medidor 
    /// Si el personaje se encuentra quieto, se sumar� el doble de la cantidad predeterminada
    /// </summary>
    private void Addvomit()
    {
        if(_frankMovement.Direction.magnitude == 0)
        {
            _vomitBar.value = _vomitBar.value + (_vomitcuantity *2);
        }
        else
        {
            _vomitBar.value = _vomitBar.value + _vomitcuantity;
        }     
    }
    /// <summary>
    /// Si el jugador realiza un Dash, el v�mito aumenta notoriamente mediante la suma de una cantidad espec�fica
    /// </summary>
    public void VomitDash()
    {
        _vomitBar.value = _vomitBar.value + _vomitDash;
    }

    /// <summary>
    /// Cada cierto tiempo se le suma una cantidad de v�mito
    /// Si el medidor llega al m�ximo, se deshabilita el Input y el Movimiento del jugador y se ejecuta la animaci�n de vomitar y el disparo del proyectil de v�mito
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _timerspeed && !_stops)
        {
            timer = 0;
            Addvomit();
        }
        if(_vomitBar.value >= _maxvomit)
        {
            _frankInput.enabled = false;
            _frankMovement.enabled = false;
            StartCoroutine(AnimVomit());
            StartCoroutine(_shootComponent.Disparo(_frankMovement._lastMovementVector));
            StartCoroutine(StopPlayer());
        }
    }
    /// <summary>
    /// Se cancelan las dem�s animaciones y se ejecuta la animaci�n de vomitar
    /// </summary>
    IEnumerator AnimVomit()
    {
        AudioManager.Instance.VomitSound();
        _animator.SetBool("Rascadita", false);
        _animator.SetBool("Tropiezo", false) ;
        _animator.SetBool("Vomito", true);
        yield return new WaitForSeconds(2f);
        _animator.SetBool("Vomito", false);
    }
    /// <summary>
    /// El jugador se detiene durante unos segundos antes de poder seguir movi�ndose
    /// </summary>
    IEnumerator StopPlayer()
    {       
        _vomitBar.value = 0;
        _stops = true;
        _targetRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(2f);
        _targetRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation; 
        _frankInput.enabled = true;
        _frankMovement.enabled = true;
        _stops = false;

    }
}
