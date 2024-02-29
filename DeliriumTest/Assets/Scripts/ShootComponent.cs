using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _bulletPrefab;
    #endregion
    public IEnumerator Disparo(Vector3 _shotDirection)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        _bullet.GetComponent<BulletComponent>().RegisterVector(_shotDirection);
        yield return new WaitForSeconds(1f);
    }

}
