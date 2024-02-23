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
    #endregion
    #region references
    private Slider _vomitBar;
    [SerializeField] private FrankMovement _frankMovement;
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
        _vomitBar.value++;           
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
            _shootComponent.Shoot();
            StartCoroutine(StopPlayer());
        }

       
    }
    IEnumerator StopPlayer()
    {
        _shootComponent.Shoot();
        _vomitBar.value = 0;
        _stops = true;
        _frankMovement.enabled = false;
        _targetRigidBody.velocity = Vector3.zero;
        yield return new WaitForSeconds(2f);
        _frankMovement.enabled = true;
        _stops = false;
    }
}
