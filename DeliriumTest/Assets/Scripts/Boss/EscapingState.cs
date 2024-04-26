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
    [SerializeField] private LayerMask layerBounds;
    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("Boss detection");
    //    FrankMovement player = collision.GetComponent<FrankMovement>();
    //    if (player != null)
    //    {
    //        bossMovement.currentSpeed = escapingValue;
    //        bossMovement.directionMovement = frankDirection.Direction.normalized;
    //        Debug.Log("Ha entrado");
    //    }
    //}
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    FrankMovement player = collision.GetComponent<FrankMovement>();
    //    if (player != null)
    //    {
    //        bossMovement.currentSpeed = 0;
    //        Debug.Log("Ha salido");
    //    }
    //}


    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("Medesplazo");
    //    Vector3 distance = _myTransform.position - FrankMovement.Player.transform.position;
    //    rb.velocity = distance * escapingValue * collision.GetContact(0).normal;
    //}

    public override void Enter()
    {
        bossMovement.currentSpeed = 0;
    }
    public override void Do()
    {
        //float escapingAngle = Random.Range(-45,45);
        //Vector3 distance = _myTransform.position - FrankMovement.Player.transform.position;
        //Collider2D paredCollider = Physics2D.OverlapBox(transform.position, boxCollider.size, layerPared);
        //Collider2D boundsCollider = Physics2D.OverlapBox(transform.position, boxCollider.size, layerBounds);
        //if (distance.magnitude < umbral && !(boundsCollider.IsTouchingLayers(layerBounds) || paredCollider.IsTouchingLayers(layerPared)))
        //{
        //    rb.velocity = distance.normalized * escapingValue;
        //}
        //else if (boundsCollider.IsTouchingLayers(layerBounds) || paredCollider.IsTouchingLayers(layerPared))
        //{

        //    rb.velocity = (Quaternion.Euler(0f, 0f, escapingAngle) * distance).normalized * - escapingValue;
        //}
        //else
        //{
        //    escapingAngle = Random.Range(-90, 90);
        //    rb.velocity = Vector2.zero;
        //}
        //isComplete = true;

        Vector3 distance = transform.position - FrankMovement.Player.transform.position;

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector3.up, Mathf.Infinity, layerBounds);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector3.down, Mathf.Infinity, layerBounds);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector3.left, Mathf.Infinity, layerBounds);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector3.right, Mathf.Infinity, layerBounds);

        if (Mathf.Abs(hitUp.distance) < boxCollider.size.magnitude / 2)
        {
            rb.AddForce(Quaternion.Euler(0f, 0f, 85f) * hitUp.normal * escapingValue * 10);
        }

        if (Mathf.Abs(hitDown.distance) < boxCollider.size.magnitude / 2)
        {
            rb.AddForce(Quaternion.Euler(0f, 0f, 85f) * hitDown.normal * escapingValue * 10);
        }

        if (Mathf.Abs(hitLeft.distance) < boxCollider.size.magnitude / 2)
        {
            rb.AddForce(Quaternion.Euler(0f, 0f, 90f) * hitLeft.normal * escapingValue * 10);
        }

        if (Mathf.Abs(hitRight.distance) < boxCollider.size.magnitude / 2)
        {
            rb.AddForce(Quaternion.Euler(0f, 0f, 90f) * hitRight.normal * escapingValue * 10);
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
