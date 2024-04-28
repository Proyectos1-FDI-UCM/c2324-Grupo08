using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class EscapingState : State
//Estado en el que huye del jugador si se acerca
{
    [SerializeField] private float escapingValue;
    [SerializeField] private float umbral;
    [SerializeField] private LayerMask layerPared;
    [SerializeField] private LayerMask layerBoundries;
    // Inicia la ani8mación respectiva e inicia la velocidad a 0
    public override void Enter()
    {
        _animation.SetBool("EscapingState", true);
        _animation.SetBool("IdleState", false);
        _animation.SetBool("AttackState", false);
        _animation.SetBool("DashState", false);
        bossMovement.currentSpeed = 0;       
    }
   // El jefe irá recogiendo la posición del jugador para poder escapar , y si se encuentra con un muro lo recorrerá ara asi evitar quedar atascado en una esquina
   // al mismo tiempoinidcará la posición que se encuetnra para poder hacer la animación
    public override void Do()
    {
        Vector3 distance = transform.position - FrankMovement.Player.transform.position;
        _animation.SetFloat("XMovement", -1);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector3.up, Mathf.Infinity, layerPared);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector3.down, Mathf.Infinity, layerBoundries);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector3.left, Mathf.Infinity, layerBoundries);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector3.right, Mathf.Infinity, layerBoundries);

        if (Mathf.Abs(hitUp.distance) < boxCollider.size.magnitude / 2)
        {
            rb.AddForce(Quaternion.Euler(0f, 0f, 85f) * hitUp.normal * escapingValue * 10);
            _animation.SetFloat("XMovement", 1);
            _animation.SetFloat("YMovement", 0);
        }

        if (Mathf.Abs(hitDown.distance) < boxCollider.size.magnitude / 2)
        {
            rb.AddForce(Quaternion.Euler(0f, 0f, 85f) * hitDown.normal * escapingValue * 10);
            _animation.SetFloat("XMovement", -1);
            _animation.SetFloat("YMovement", 0);
        }

        if (Mathf.Abs(hitLeft.distance) < boxCollider.size.magnitude / 2)
        {
            rb.AddForce(Quaternion.Euler(0f, 0f, 90f) * hitLeft.normal * escapingValue * 10);
            _animation.SetFloat("XMovement", 0);
            _animation.SetFloat("YMovement", 1);
        }

        if (Mathf.Abs(hitRight.distance) < boxCollider.size.magnitude / 2)
        {
            rb.AddForce(Quaternion.Euler(0f, 0f, 90f) * hitRight.normal * escapingValue * 10);
            _animation.SetFloat("XMovement", 0);
            _animation.SetFloat("YMovement", - 1);
        }

        if (Mathf.Abs(distance.magnitude) < umbral)
        {
            rb.velocity = distance.normalized * escapingValue;
        }

        else
        {
            rb.velocity = Vector3.zero;
        }
        isComplete = true; 
    }
}
