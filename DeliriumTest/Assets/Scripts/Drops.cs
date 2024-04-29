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
        if (healthComponent.Health <= 0)
        {
            dropNumber = Random.Range(1, 21);
            if (dropNumber <= 3 && dropNumber > 0)
            {
                Instantiate(drops[1], transform.position, Quaternion.identity);
            }
            else if (dropNumber <= 6)
            {
                Instantiate(drops[0], transform.position, Quaternion.identity);
            }
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
