using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Collider2D Collider2D;
    Rigidbody2D _myRigidbody;
    Rigidbody2D _playerRigidbody;
    float offsetx;
    float offsety;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D = GetComponent<Collider2D>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody = FrankMovement.Player.GetComponent<Rigidbody2D>();
        Collider2D.enabled = false;
    }
    public IEnumerator Attack()
    {
        transform.position = FrankMovement.Player.transform.position + new Vector3(offsetx, offsety);
        Collider2D.enabled = true;
        yield return new WaitForFixedUpdate();
        Collider2D.enabled = false;
        StopCoroutine(Attack());
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
