using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Rigidbody2D _rigidBody;
    #endregion
    #region references
    #endregion
    #region propiedades
    #endregion
    // Start is called before the first frame update
    private FrankMovement _frankMovement;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _frankMovement._lastMovementVector * _bulletSpeed;
    }

    void Shoot()
    {
        Instantiate(_bulletPrefab, transform);
    }
}
