using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeBarComponenet : MonoBehaviour
{
    #region references
    public GameObject healthPrefab;
    public PlayerHealth playerHealth;
    #endregion
    #region properties
    List<LifeComponent> hearts= new List<LifeComponent> ();
    #endregion
    private void OnEnable()
    {
         PlayerHealth.OnPlayerDamaged += DrawHearts;

    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= DrawHearts;      
    }
    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = playerHealth.maxHealth % 2;
        int heartToMake = (int)(playerHealth.maxHealth / 2 + maxHealthRemainder);
        for(int i = 0;i< heartToMake;i++) 
        {
            CreateEmptyHeart();
        }
        for(int i = 0; i< hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.health-(i*2), 0, 2);
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
    }
}
