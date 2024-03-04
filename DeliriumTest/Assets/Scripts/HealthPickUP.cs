using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUP : MonoBehaviour
{

    public LifeBarComponenet lifeBar;
    [SerializeField] private float healing;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FrankMovement player = collision.gameObject.GetComponent<FrankMovement>();
        if (player != null)
        {
            HealthComponent health = collision.gameObject.GetComponent<HealthComponent>();
            if (health.Health != health.MaxHealth)
            {
                health.Healing(healing);
                lifeBar.DrawHearts();
                Destroy(this.gameObject);
            }
        }

    }
    public void RegisterLifeBar(LifeBarComponenet lifeBarComponent)
    { 
        lifeBar = lifeBarComponent;
    }
}

