using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private bool isFacingRIght = true;
    
    //Esto hace que el enemigo mire en la dirección en la que está el jugador
    private void Flip(bool isPlayerRight)
    {
        //Comprueba el lado al que mira y si el jugador se mueve a la izda. se gira a esa dirección
        if(isFacingRIght && !isPlayerRight|| !isFacingRIght && isPlayerRight) 
        {
            isFacingRIght = !isFacingRIght;
            Vector3 scale =transform.localScale;
            scale.x *= -1;
            transform.localScale =scale; 
        }
    }
    private void Update()
    {
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);
    }
}
