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
    public IEnumerator Attack(Animator _animator)
    {
        transform.position = FrankMovement.Player.transform.position + new Vector3(offsetx, offsety);
        Collider2D.enabled = true;
        _spriteRenderer.enabled = true;
        //for(int i = duraciondeataque; i > 0; i--) yield return new WaitForFixedUpdate();
        
        _animator.SetBool("Attack", true);
        yield return null;
        _animator.SetBool("Attack", false);
        Collider2D.enabled = false;
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.80f);
        //StopCoroutine(Attack());
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
