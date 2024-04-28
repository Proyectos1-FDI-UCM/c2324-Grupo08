using UnityEngine;

public class Recogible : MonoBehaviour
{
    // En función del objeto que ha recgido el jugador y el ID que tiene se destruirá/"desaparecerá" el que ha recogido y hará la lógica correspondiente
    
    [SerializeField] private int _objectID;
    /* 
     * 1 = Bolsa de Patatas
     * 2 = Botella de Agua
     * 3 = Kebab
     * 4 = Bebida Energética
     * 5 = Cubo de Chapas
     * 6 = ArmaBoss;
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RecogerObjeto recogida = collision.GetComponent<RecogerObjeto>();
        if (recogida != null && _objectID != 6)
        {
            recogida.Recogida(_objectID, this.gameObject);
        }
        else if (collision.GetComponent<BossController>() != null && _objectID == 6) // Lógica delboss para hacer desaparecer el arma recogible del boss
        {
            Destroy(this.gameObject);
            collision.GetComponentInChildren<ShootingState>()._bulletHit = false; 
        }
      
    }
}
