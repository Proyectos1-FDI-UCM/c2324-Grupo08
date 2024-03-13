using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeBarComponenet : MonoBehaviour
{
    #region references
    public GameObject healthPrefab;
    public HealthComponent playerHealth;
    #endregion
    #region properties
    List<LifeComponent> hearts = new List<LifeComponent> ();
    #endregion
    private void OnEnable()
    {
         HealthComponent.OnPlayerDamaged += DrawHearts;

    }
    private void OnDisable()
    {
        HealthComponent.OnPlayerDamaged -= DrawHearts;      
    }
    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = playerHealth.MaxHealth % 2;
        int heartToMake = (int)(playerHealth.MaxHealth / 2 + maxHealthRemainder);
        for(int i = 0;i< heartToMake;i++) 
        {
            CreateEmptyHeart();
        }
        for(int i = 0; i< hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.Health-(i*2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void CreateEmptyHeart()
    {
        GameObject newheart = Instantiate(healthPrefab);
        newheart.transform.SetParent(transform);
        LifeComponent healthComponent = newheart.GetComponent<LifeComponent>();
        healthComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(healthComponent);
    }
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts= new List<LifeComponent>();
    }

    
    private void Start()
    {
        DrawHearts();
        playerHealth = FrankMovement.Player.GetComponent<HealthComponent>();
        if (playerHealth == null) Debug.LogError("No se encontró un componente de vida en el Jugador");
    }
}
