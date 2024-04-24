using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CircleCollider2D collider;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask layerPared;
    [SerializeField] private LayerMask layerBounds;
    [SerializeField] private GameObject botella;
    // Start is called before the first frame update
    void Start()
    {
      collider = GetComponent<CircleCollider2D>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Physics2D.OverlapCircle(transform.position, collider.radius * 1.1f,layerBounds) != null || 
            Physics2D.OverlapCircle(transform.position, collider.radius * 1.1f, layerPared) != null) 
        {
            rb.velocity = Vector3.zero;
            Instantiate(botella, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
