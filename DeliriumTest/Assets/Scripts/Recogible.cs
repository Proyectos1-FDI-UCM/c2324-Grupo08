using UnityEngine;

public class Recogible : MonoBehaviour
{
    // En funci�n del objeto que ha recgido el jugador y el ID que tiene se destruir�/"desaparecer�" el que ha recogido y har� la l�gica correspondiente
    
    [SerializeField] private int _objectID;
    /* 
     * 1 = Bolsa de Patatas
     * 2 = Botella de Agua
     * 3 = Kebab
     * 4 = Bebida Energ�tica
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
        else if (collision.GetComponent<BossController>() != null && _objectID == 6) // L�gica delboss para hacer desaparecer el arma recogible del boss
        {
            Destroy(this.gameObject);
            collision.GetComponentInChildren<ShootingState>()._bulletHit = false; 
        }
      
    }
}
