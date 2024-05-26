using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _bulletSpeed = 10f;
    private Rigidbody2D _rigidBody;
    private Vector3 _speed;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Se llama al instanciarse la bala. 
    /// Establece su dirección con el vector direction.
    /// Rota desde su izquierda hacia la dirección en la que es propulsado en el eje z.
    /// </summary>
    /// <param name="direction"></param>
    public void RegisterVector(Vector3 direction)
    {
        if(Mathf.Abs(Vector3.Angle(Vector3.left,direction)) > 10f)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
            //Por si hay alguna rotación en los ejes x o y lo eliminamos para evitar que se
            //aplanen los proyectiles.
            transform.rotation = new Quaternion(0f, 0f, transform.rotation.z, transform.rotation.w);
        }
        else
        {
            //Para la rotar el vomito a la izquierda, si iba en esa dirección.
            transform.rotation = new Quaternion(0f, 0f, 180f, transform.rotation.w);
        }
        //Se establece la dirección
        _speed = direction;

    }
    /// <summary>
    /// El objeto al colisionar con el personaje, los enemigos o los bordes, se destruye
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<FrankMovement>() != null || collision.gameObject.GetComponent<EnemiesControler>() != null || collision.gameObject.layer == 7 || collision.gameObject.layer == 9)
            Destroy(gameObject);
    }

    /// <summary>
    /// Variable que guarda cunato tiempo esta colisionando con algún elemento.
    /// Sirve para controlar el metodo OnCollisionStay2D.
    /// </summary>
    float timeForDestroy;

    /// <summary>
    /// Al colisionar con algún objeto no previsto, si su velocidad es cero, se destruye.
    /// Tambien si esta colisionando con algún objeto por más de 2.5 seg desaparece
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        timeForDestroy += Time.deltaTime;
        if (_rigidBody.velocity.Equals((Vector3.zero)) || timeForDestroy >= 2.5f)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    /// <summary>
    /// Mueve por fisicas la bala según su dirección y su velocidad
    /// </summary>
    void FixedUpdate()
    {

        _rigidBody.velocity = _speed * 10f * _bulletSpeed * Time.fixedDeltaTime;

    }
}
