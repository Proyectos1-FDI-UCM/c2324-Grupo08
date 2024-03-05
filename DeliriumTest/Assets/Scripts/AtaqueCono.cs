using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Cono : MonoBehaviour
{
    #region parameters
    [SerializeField] private GameObject ataque;
    [SerializeField] private GameObject ataqueCono;
    private PlayerAttack _attack;
    [SerializeField] private RecogerObjetos _recogerObjetos;
    Damage _damage;
    #endregion
    #region referneces
    [SerializeField]
    float offsetx = 0.7f;
    [SerializeField]
    float offsety = 0.7f;
    float finaloffset;
    [SerializeField] int duraciondeataque;
    #endregion
    #region properties
    BoxCollider2D Collider2D;
    Rigidbody2D _myRigidbody;
    Rigidbody2D _playerRigidbody;
    SpriteRenderer _spriteRenderer;
    #endregion

    public IEnumerator AtaqueCono()
    {
        transform.position = FrankMovement.Player.transform.position + new Vector3(offsetx, offsety);
        Collider2D.enabled = true;
        _spriteRenderer.enabled = true;
        for (int i = duraciondeataque; i > 0; i--) yield return new WaitForFixedUpdate();
        Collider2D.enabled = false;
        _spriteRenderer.enabled = false;
        ataqueCono.SetActive(false);
        ataque.SetActive(true);
        StopCoroutine(AtaqueCono());
    }
    public void Setoffsetx(float value)
    {
        offsetx = value;
    }
    public void Setoffsety(float value)
    {
        offsety = value;
    }
    private void Start()
    {
        _attack = GetComponent<PlayerAttack>();
        _damage = GetComponent<Damage>();
        Collider2D = GetComponent<BoxCollider2D>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody = FrankMovement.Player.GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Setoffsetx(0);
            if (Input.GetKeyDown(KeyCode.DownArrow)) finaloffset = -offsety;
            else finaloffset = offsety;
            Setoffsety(finaloffset);
            StartCoroutine(AtaqueCono());
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) finaloffset = -offsetx;
            else finaloffset = offsetx;
            Setoffsetx(finaloffset);
            Setoffsety(0);
            StartCoroutine(AtaqueCono());
        }
    }
}

