using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarolaComponet : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    private Transform target;
    private ShootComponent _shootComponent;
    #endregion

    #region properties
    private Vector3 _direction;
    private bool _PacoColision;
    [SerializeField] private float fireRate;
    private float fireTime;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null)
        {
            _PacoColision = true;
            target = other.gameObject.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null)
        {
            _PacoColision = false;
        }

    }

    void Start()
    {
        _shootComponent = GetComponent<ShootComponent>();
        _myTransform = transform;
        _PacoColision = false;
        fireTime = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (_PacoColision)
        {
            fireTime -= Time.deltaTime;

            if (fireTime <= 0)
            {
                _direction = (target.position - _myTransform.position).normalized;
                StartCoroutine(_shootComponent.Disparo(_direction));
                fireTime = fireRate;
            }

        }


    }
}
