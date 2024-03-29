using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotellaLanzable : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject ataque;
    [SerializeField] private GameObject ataqueBotella;
    [SerializeField] private Recolector_de_Armas _recogerObjetos;
    public float BottleOffsetinx;
    public float BottleOffsetiny;
    Damage _damage;
    [SerializeField]
    float offsetx = 0.7f;
    [SerializeField]
    float offsety = 0.7f;
    float finaloffset;
    private FrankMovement _direction;
    private PlayerAttack _attack;
    private Vector2 ShootVector;
    
    public void AtaqueBotella(Vector3 _shootDirection)
    {        
        GameObject _bullet = Instantiate(_bulletPrefab, transform.position + _shootDirection, Quaternion.identity);
        _bullet.GetComponent<BulletComponent>().RegisterVector(_shootDirection);
        ataqueBotella.SetActive(false);
        ataque.SetActive(true);
        _recogerObjetos.canBePicked = true;
        UIManager.UiManager.QuitarSpriteBotella();
    }
    private void Awake()
    {
        _direction= GetComponentInParent<FrankMovement>();
    }
    private void Start()
    {
        ataqueBotella.SetActive(false);
    }

    public void Setoffsetx(float value)
    {
        offsetx = value;
    }
    public void Setoffsety(float value)
    {
        offsety = value;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Setoffsetx(0);
            if (Input.GetKeyDown(KeyCode.DownArrow)) finaloffset = -offsety;
            else finaloffset = offsety;
            Setoffsety(finaloffset);
            ShootVector = new Vector2(offsetx,offsety);
            AtaqueBotella(ShootVector);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) finaloffset = -offsetx;
            else finaloffset = offsetx;
            Setoffsetx(finaloffset);
            Setoffsety(0);
            ShootVector = new Vector2(offsetx,offsety);
            AtaqueBotella(ShootVector);
        }
    }
}
