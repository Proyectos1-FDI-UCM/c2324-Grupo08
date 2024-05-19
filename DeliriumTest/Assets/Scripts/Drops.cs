using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// Lista que contiene todos los posibles drops.
    /// </summary>
    [SerializeField] private List<GameObject> drops;

    /// <summary>
    /// Número que guarda un número aleatorio del 0 al 99.
    /// </summary>
    private int dropNumber;
    #endregion

    #region Methods
    /// <summary>
    /// Generará un número aleatorio entre el 0 al 99
    /// Si el número se encuentra por debajo de 30 
    /// instancia un objeto aleatorio dentro de los drops.
    /// </summary>
    public void Drop()
    { 
            //Genera un número aleatorio entre 0-99
            dropNumber = Random.Range(0, 100);
            
            //Si es menor a treinta y la lista no esta vacia instancia un drop aleatorio.
            //Esto hace que se genere un drop en un 30% de los enemigos.
            if (dropNumber <= 30 && drops.Count > 0)
            {
                
                Instantiate(drops[Random.Range(0, drops.Count - 1)], transform.position, Quaternion.identity);
            }

            //Destruye al enemigo que dropeo algo.
            Destroy(this.gameObject);
    }
    #endregion
}
