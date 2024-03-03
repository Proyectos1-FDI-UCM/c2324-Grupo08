using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    BoxCollider2D Collider2D;
    Rigidbody2D _myRigidbody;
    Rigidbody2D _playerRigidbody;
    SpriteRenderer _spriteRenderer;
    [SerializeField] int duraciondeataque;  
    float offsetx;
    float offsety;
    Damage attack;
    private RecogerObjetos recogerObjetos;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D = GetComponent<BoxCollider2D>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody = FrankMovement.Player.GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        attack= GetComponent<Damage>();
        Collider2D.enabled = false;
    }
    public IEnumerator Attack()
    {
        transform.position = FrankMovement.Player.transform.position + new Vector3(offsetx, offsety);
        Collider2D.enabled = true;
        _spriteRenderer.enabled = true;
        for(int i = duraciondeataque; i > 0; i--) yield return new WaitForFixedUpdate();
        Collider2D.enabled = false;
        _spriteRenderer.enabled= false;
        AtaqueBasico();
        StopCoroutine(Attack());
    }
    public void AtaqueCono()
    {
        attack.Attack = 4;
        Collider2D.size = new Vector2(1.3f,1.5f);
        offsetx = 1f;
        offsety = 1f;
        recogerObjetos.canBePicked = true;
    }
    public void AtaqueBasico()
    {
        attack.Attack = 2;
        Collider2D.size = new Vector2(1f, 1f);
        offsetx = 0.7f;
        offsety = 0.7f;
    }
    void FixedUpdate()
    {
        _myRigidbody.velocity = _playerRigidbody.velocity;
    }
    public void Setoffsetx(float value)
    {
        offsetx = value;
    }
    public void Setoffsety(float value)
    {
        offsety = value;
    }
}
