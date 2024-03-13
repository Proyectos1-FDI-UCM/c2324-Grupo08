using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUP : MonoBehaviour
{

    
    [SerializeField] private float healing;
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        FrankMovement player = collision.gameObject.GetComponent<FrankMovement>();
        if (player != null)
        {
            HealthComponent health = collision.gameObject.GetComponent<HealthComponent>();
            if (health.Health != health.MaxHealth)
            {
                health.Healing(healing);               
                Destroy(this.gameObject);
            }
        } 
    }
    */
    /*public void RegisterLifeBar(LifeBarComponenet lifeBarComponent)
    { 
        lifeBar = lifeBarComponent;
    }*/
}

