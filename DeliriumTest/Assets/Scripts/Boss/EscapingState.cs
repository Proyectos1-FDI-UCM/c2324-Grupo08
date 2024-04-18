using UnityEngine;

public class EscapingState : State
//Estado en el que huye del jugador si se acerca
{
    [SerializeField] private float escapingValue;
    [SerializeField] private float umbral;
    private Transform _myTransform;
    /*void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Boss detection");
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            bossMovement.currentSpeed = escapingValue;
            bossMovement.directionMovement = frankDirection.Direction.normalized;
            Debug.Log("Ha entrado");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            bossMovement.currentSpeed = 0;
            Debug.Log("Ha salido");
        }
    }*/
    public override void Enter()
    {
        bossMovement.currentSpeed = 0;
        _myTransform = transform;
    }
    public override void Do()
    {
        Vector3 distance = _myTransform.position - FrankMovement.Player.transform.position;
        if (distance.magnitude < umbral)
        {
            rb.velocity = distance * escapingValue;
        }
        else rb.velocity = Vector3.zero;
        isComplete = true;
    }
}
