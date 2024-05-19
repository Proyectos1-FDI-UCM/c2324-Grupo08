using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    //Componentes fisicos propios
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;

    //Reconocer cuales son los límites del nivel (bounds)
    [SerializeField] private LayerMask layerPared;
    [SerializeField] private LayerMask layerBounds;
    [SerializeField] private GameObject botella;

    //Shooting del cual a sido invocado
    public ShootingState shootingState; 

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    //Aqui se comprueba si el collider de la botella a colisionado con algun tipo d ebound para evitar que se vaya de la vista del jugador
    void FixedUpdate()
    {
        if(Physics2D.OverlapCircle(transform.position, circleCollider.radius * 1.1f,layerBounds) != null || 
            Physics2D.OverlapCircle(transform.position, circleCollider.radius * 1.1f, layerPared) != null) 
        {
            rb.velocity = Vector3.zero;
            Destroy(gameObject);
        }
    }
    // Aqui comprueba si ha impactado la bottella y además cambia el proyectil por el arma recogible del boss
    private void OnDestroy()
    {
        GameObject Bullet = Instantiate(botella, transform.position, Quaternion.identity);
        if(Bullet == null)
        {
            Debug.Log("No hay instance");
        }
        else
        {
            shootingState._bulletHit = true;
            shootingState._bullet = Bullet;
            
        }
        
    }
}
