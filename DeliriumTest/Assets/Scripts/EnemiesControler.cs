using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AVISO: Esta interfaz ha sido creada para indicar que objetos son enemigos 
public interface EnemiesControler
{
    /// <summary>
    /// Corutine que realiza la parada del ataque 
    /// </summary>
    /// <returns>Se detiene un numero de segundo</returns>
    public IEnumerator StopAttack();
}
