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
    /// <summary>
    /// Crea el total de vida que tiene el jugador en ese momento por medio de una llamada a un evento de Unity
    /// </summary>
    private void OnEnable()
    {
         HealthComponent.OnPlayerDamaged += DrawHearts;

    }
    private void OnDisable()
    {
        HealthComponent.OnPlayerDamaged -= DrawHearts;      
    }
    /// <summary>
    /// Inicialmente creará cuantos corazones hay y en función del parámetro de vida del HealthComponenet pondrá la vida llena,media o vacía
    /// </summary>
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
    /// <summary>
    /// Este método crea un corazón vació y recoge la información del prefab de vida el cual puede cambiar entre lleno,medio o vacio
    /// </summary>
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
    /// <summary>
    /// Método que permite aumentar la vida total
    /// </summary>
    public void HealthUP()
    {
        playerHealth.Health += 2;
        playerHealth.MaxHealth += 2;
        DrawHearts();
    }
    
    private void Start()
    {
        DrawHearts();
        playerHealth = FrankMovement.Player.GetComponent<HealthComponent>();
        if (playerHealth == null) Debug.LogError("No se encontró un componente de vida en el Jugador");
    }
    private void Update()
    {
        DrawHearts();
    }
}
