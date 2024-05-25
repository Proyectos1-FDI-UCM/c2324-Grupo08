using UnityEngine;

//Estado en el que huye del jugador si se acerca
public class EscapingState : State
{
    #region References
    /// <summary>
    /// Capas donde se encuentran los limites del mapa.
    /// </summary>
    [SerializeField] private LayerMask layerPared;
    [SerializeField] private LayerMask layerBoundries;
    #endregion

    #region Properties
    /// <summary>
    /// Velocidad a la que escapa el boss de Paco.
    /// </summary>
    [SerializeField] private float escapingValue;

    /// <summary>
    /// Distancia a la que el boss comienza a escapar.
    /// </summary>
    [SerializeField] private float umbral;

    /// <summary>
    /// Comprobante que marca si el boss esta recorriendo una pared y no podria escapar.
    /// </summary>
    bool running;

    /// <summary>
    /// Vector normalizado que apunta a la pared más cercana.
    /// </summary>
    Vector3 walldirection;

    /// <summary>
    /// Tiempo que ha estado el boss recorriendo una pared
    /// </summary>
    float runningtime;

    /// <summary>
    /// Angulo con el que el boss rodea una pared
    /// </summary>
    float runningangle;
    #endregion

    // Inicia la animación respectiva e inicia la velocidad a 0.
    public override void Enter()
    {
        _animation.SetBool("EscapingState", true);
        _animation.SetBool("IdleState", false);
        _animation.SetBool("AttackState", false);
        _animation.SetBool("DashState", false);
        bossMovement.currentSpeed = 0;
        running = false;
    }

    /// <summary>
    /// El jefe irá recogiendo la posición del jugador para poder escapar y 
    /// si se encuentra con un muro lo recorrerá para asi evitar quedar atascado en una esquina.
    /// Al mismo tiempo indicará la posición que se encuentra para poder hacer la animación.
    /// </summary>
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

    /// <summary>
    /// El boss huira de Paco mientras comprueba cual es la pared más cercana
    /// y la distancia con esta para recorrerla y evitar ser acorralado.
    /// Una vez detecte una pared la recorre por 2.0 segundos y cambia el angulo por 
    /// uno aleatorio entre 25º y 65º para dejar de dar vueltas. Tras 2.0 segundo el angulo se
    /// reestablece a 90º.
    /// Durante todo este proceso el boss tratará de huir de Paco siempre que encuentre una salida
    /// (No haya una pared en su trayectoria).
    /// </summary>
    public override void Do()
    {
        Vector3 distance = transform.position - FrankMovement.Player.transform.position;


        if (running)
        {
            runningtime += Time.fixedDeltaTime;
            if (runningtime >= 2.0f)
            {
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
                runningangle = -Random.Range(45f, 90f) * Mathf.Sign(runningangle);
            }
            CheckClosestWall(distance);
        }
        if (rb.velocity.magnitude > Mathf.Epsilon)
        {
            _animation.SetFloat("XMovement", walldirection.x);
            _animation.SetFloat("YMovement", walldirection.y);
        }
        else
        {
            _animation.SetFloat("XMovement", 0f);
            _animation.SetFloat("YMovement", 0f);
        }
        isComplete = true;
        
    }
}
