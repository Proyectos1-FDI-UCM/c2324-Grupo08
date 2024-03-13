using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    [SerializeField] private GameObject[] drops;
    private int dropNumber;
    private HealthComponent healthComponent;


    public void Drop()
    {
        Debug.Log("Entra");
        if (healthComponent.Health <= 0)
        {
            dropNumber = Random.Range(1, 21);
            if (dropNumber <= 3)
            {
                Debug.Log("Drop");
                Instantiate(drops[1], transform.position, Quaternion.identity);
            }
            else if (dropNumber <= 6)
            {
                Instantiate(drops[0], transform.position, Quaternion.identity);
                Debug.Log("Drop");
            }
            Debug.Log("Entra");
            Destroy(this.gameObject);
        }

        
    }




    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
