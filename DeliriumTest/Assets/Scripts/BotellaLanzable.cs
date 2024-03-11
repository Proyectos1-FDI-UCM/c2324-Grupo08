using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotellaLanzable : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject ataque;
    [SerializeField] private GameObject ataqueBotella;
    [SerializeField] private RecogerObjetos _recogerObjetos;
    Damage _damage;
    private FrankMovement _direction;
    private PlayerAttack _attack;
    
    public void AtaqueBotella(Vector3 _shootDirection)
    {
        Debug.Log("Botella lanzada");
        GameObject _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        _bullet.GetComponent<BulletComponent>().RegisterVector(_shootDirection);
        /*ataqueBotella.SetActive(false);
        ataque.SetActive(true);
        _recogerObjetos.canBePicked = true;*/
    }
    private void Awake()
    {
        _direction= GetComponentInParent<FrankMovement>();
    }
    private void Start()
    {
        ataqueBotella.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            AtaqueBotella(_direction._lastMovementVector);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            AtaqueBotella(_direction._lastMovementVector);
        }
    }
}
