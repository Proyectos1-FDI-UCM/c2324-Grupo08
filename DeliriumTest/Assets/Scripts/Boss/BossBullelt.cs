using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask layerPared;
    [SerializeField] private LayerMask layerBounds;
    [SerializeField] private GameObject botella;
    private ShootingState shootingState; 
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Physics2D.OverlapCircle(transform.position, circleCollider.radius * 1.1f,layerBounds) != null || 
            Physics2D.OverlapCircle(transform.position, circleCollider.radius * 1.1f, layerPared) != null) 
        {
            rb.velocity = Vector3.zero;
            shootingState._bullet = Instantiate(botella, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
