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
    #endregion
    #region propiedades
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _vomitBar = GetComponent<Slider>();
        _vomitBar.value = 0;
    }

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

    public void VomitDash()
    {
        _vomitBar.value = _vomitBar.value + _vomitDash;
    }

    // Update is called once per frame
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
            StartCoroutine(_shootComponent.Disparo(_frankMovement._lastMovementVector));
            StartCoroutine(StopPlayer());
        }

       
    }
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
