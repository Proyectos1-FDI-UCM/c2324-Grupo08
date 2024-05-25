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
    /// <summary>
    /// Detecta si el ersonaje está en rango para poder atacar
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null)
        {
            _PacoColision = true;
            target = other.gameObject.transform;
        }

    }
    /// <summary>
    /// Detecta la salida del personaje de rango
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Si está en rango, se dispara un proyectil hacia Paco cada cierto tiempo
    /// </summary>
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
