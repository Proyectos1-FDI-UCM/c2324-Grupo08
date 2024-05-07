using UnityEngine;

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
        running = false;
    }
    // El jefe irá recogiendo la posición del jugador para poder escapar y si se encuentra con un muro lo recorrerá para asi evitar quedar atascado en una esquina
    // al mismo tiempo indicará la posición que se encuentra para poder hacer la animación
    bool running;
    Vector3 walldirection;

    void CheckClosestWall(Vector3 distance)
    {
        float angle = Vector2.Angle(Vector2.up, distance);
        float angleDown = Vector2.Angle(Vector2.down, distance);
        float angleLeft = Vector2.Angle(Vector2.left, distance);
        float angleRight = Vector2.Angle(Vector2.right, distance);
        walldirection = Vector3.up;
        if (angle > angleDown)
        {
            angle = angleDown;
            walldirection = Vector3.down;
        }
        if (angle > angleLeft)
        {
            angle = angleLeft;
            walldirection = Vector3.left;
        }
        if (angle > angleRight)
        {
            walldirection = Vector3.right;
        }
    }

    float runningtime;
    float runningangle;
    public override void Do()
    {
        Vector3 distance = transform.position - FrankMovement.Player.transform.position;


        if (running) 
        { 
            runningtime += Time.fixedDeltaTime;
            if (runningtime >= 2.0f)
            {
                Debug.Log("Fin carrera");
                runningtime = 0f;
                if (runningangle != 90)
                {
                    runningangle = 90f;
                    running = false;
                    CheckClosestWall(walldirection);
                }
                else
                {
                    runningangle = Random.Range(25f, 65f);
                }
            }
        }
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, walldirection, Mathf.Infinity, layerPared + layerBoundries);

        if (hit.collider) Debug.DrawRay(transform.position, walldirection, Color.yellow);
        else Debug.DrawRay(transform.position, walldirection, Color.gray);

        if (Mathf.Abs(hit.distance) < boxCollider.size.magnitude / 2)
        {
            running = true;
            walldirection = Quaternion.Euler(0f, 0f, runningangle) * hit.normal;
            rb.velocity = walldirection * escapingValue;
        }

        else if (Mathf.Abs(distance.magnitude) < umbral && !running)
        {
            if (!running) 
            {
                rb.velocity = distance.normalized * escapingValue;
                runningangle = -Random.Range(45f, 90f)* Mathf.Sign(runningangle); 
            }
            CheckClosestWall(distance);
        }
        if(rb.velocity.magnitude > Mathf.Epsilon) 
        {
            _animation.SetFloat("XMovement", walldirection.x);
            _animation.SetFloat("YMovement", walldirection.y);
        }
        else
        {
            _animation.SetFloat("XMovement", 0f);
            _animation.SetFloat("YMovement", 0f);
        }
        

        // if (Mathf.Abs(hitUp.distance) < boxCollider.size.magnitude / 2)
        // {
        //     rb.velocity = Quaternion.Euler(0f, 0f, 90f) * hitUp.normal * escapingValue;
        //     _animation.SetFloat("XMovement", 1);
        //     _animation.SetFloat("YMovement", 0);
        // }

        //if (Mathf.Abs(hitDown.distance) < boxCollider.size.magnitude / 2)
        // {
        //     rb.velocity = Quaternion.Euler(0f, 0f, 90f) * hitDown.normal * escapingValue;
        //     _animation.SetFloat("XMovement", -1);
        //     _animation.SetFloat("YMovement", 0);
        // }

        // if (Mathf.Abs(hitLeft.distance) < boxCollider.size.magnitude / 2)
        // {
        //     rb.velocity = Quaternion.Euler(0f, 0f, 90f) * hitLeft.normal * escapingValue;
        //     _animation.SetFloat("XMovement", 0);
        //     _animation.SetFloat("YMovement", 1);
        // }

        // if (Mathf.Abs(hitRight.distance) < boxCollider.size.magnitude / 2)
        // {
        //     rb.velocity = Quaternion.Euler(0f, 0f, 90f) * hitRight.normal * escapingValue;
        //     _animation.SetFloat("XMovement", 0);
        //     _animation.SetFloat("YMovement", - 1);
        // }

        //else
        //{
        //    rb.velocity = Vector3.zero;
        //    _animation.SetFloat("XMovement", 0);
        //    _animation.SetFloat("YMovement", 0);
        //}
        isComplete = true;
    }
}
