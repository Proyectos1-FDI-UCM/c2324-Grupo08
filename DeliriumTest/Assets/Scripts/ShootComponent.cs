using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _bulletPrefab;
    #endregion
    #region parameters
    #endregion
    /// <summary>
    /// Crea una instancia de una bala con el prefab referenciado, y accede a su componente de bala para registrar una dirección.
    /// </summary>
    /// <param name="_shotDirection"></param>
    /// <returns></returns>
    public IEnumerator Disparo(Vector3 _shotDirection)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        _bullet.GetComponent<BulletComponent>().RegisterVector(_shotDirection);
    }

}
