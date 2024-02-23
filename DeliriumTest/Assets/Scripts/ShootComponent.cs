using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    #region parameters

    #endregion
    #region references
    private FrankMovement _frankMovement;
    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _bullet;

    #endregion
    #region propiedades
    #endregion
    // Start is called before the first frame update

    private Vector3 _shotDirection;
    void Start()
    {
        _frankMovement = GetComponent<FrankMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    public void Shoot()
    {
        _shotDirection = _frankMovement._lastMovementVector;
        _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        _bullet.GetComponent<BulletComponent>().RegisterVector(_shotDirection);
    }
}
