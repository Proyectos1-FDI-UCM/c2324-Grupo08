using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Cono : MonoBehaviour
{
    #region referneces
    [SerializeField] private GameObject ataque;
    [SerializeField] private GameObject ataqueCono;
    private PlayerAttack _attack;
    [SerializeField] private RecogerObjetos _recogerObjetos;
    Damage _damage;
    private Animator animacionCono;
    #endregion
    #region parameters
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
        _recogerObjetos.canBePicked = true;
        animacionCono.SetBool("CambioAtaque", false);
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
    private void Awake()
    {
        animacionCono = GetComponentInParent<Animator>();
    }
    private void Start()
    {
        ataqueCono.SetActive(false);
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
            animacionCono.SetFloat("X", 0);
            if (finaloffset < 0) animacionCono.SetFloat("Y", -1);
            else animacionCono.SetFloat("Y", 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) finaloffset = -offsetx;
            else finaloffset = offsetx;
            Setoffsetx(finaloffset);
            Setoffsety(0);
            StartCoroutine(AtaqueCono());
            animacionCono.SetFloat("Y", 0);
            if (finaloffset < 0) animacionCono.SetFloat("X", -1);
            else animacionCono.SetFloat("X", 1);
        }
    }
}

