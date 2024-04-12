using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region References

    //Componentes necesarios:
    BoxCollider2D Collider2D;
    SpriteRenderer _spriteRenderer;
    Damage attack;
    Recolector_de_Armas _recolector;
    ShootComponent _shootComponent;
    #endregion

    #region Properties

    /// <summary>
    /// El número de FixedUpdates que esperará la corutina
    ///antes de deshabilitar el ataque
    ///</summary>
    [SerializeField] int duraciondeataque;

    /// <summary>
    ///Floats que contienen las componentes del Vector Offset 
    /// que marca la dirección del ataque:
    /// </summary>
    private float offsetx;
    private float offsety;

    //Comprobación de los tipos de ataque:
    //Mejora (Cubo de Chapas)
    [HideInInspector]
    public bool _mejorado;
    //Cono
    [HideInInspector]
    public bool _cono;
    //Botella
    [HideInInspector]
    public bool _botella;
    #endregion

    #region Methods

    //Métodos que cambian el ataque y sus propiedades
    #region Set type of Attack

    public void Chapas()
    {
        //Activación del comprobante de la mejora (Cubo de Chapas)
        _mejorado = true;

        //Cambio de propiedades acordes al uso de la mejora (Cubo de Chapas)
        attack.Attack = 1; //Daño realizado
        duraciondeataque = 2; //Duración del ataque
    }

    public void Basico()
    {
        //Cambio de propiedades acordes al ataque por defecto (Básico)
        attack.Attack = 2; //Daño realizado
        duraciondeataque = 2; //Duración del ataque
        Collider2D.size = new Vector2(1, 1); //Tamaño por defecto del collider
    }

    public void Cono()
    {
        //Activación del comprobante del uso del cono
        _cono = true;

        //Cambio de propiedades acordes al cono
        attack.Attack = 4; //Daño realizado
        duraciondeataque = 3; //Duración del ataque
        Collider2D.size = new Vector2(1.5f, 1.5f); //Tamaño del collider para el cono
    }
    public void Botella()
    {
        //Activación del comprobante de la botella
        _botella = true;

        //Cambio de propiedades acordes al botella
        attack.Attack = 3; //Daño realizado
    }
    #endregion

    //Métodos que asignan el valor de la dirección de ataque
    #region Set direction of attack

    public void Setoffsetx(float value)
    {
        //Set básico de la distancia del ataque
        offsetx = value;

        //Ajuste de la distancia de ataque para los diferentes ataques:
        //Mejora(Cubo de Chapas)
        if (_mejorado)
        {
            offsetx *= 2;
        }

        //Cono
        else if (_cono)
        {
            offsetx += 0.3f;
        }
    }

    public void Setoffsety(float value)
    {
        //Set básico de la distancia del ataque
        offsety = value;

        //Ajuste de la distancia de ataque para los diferentes ataques:
        //Mejora(Cubo de Chapas)
        if (_mejorado)
        {
            offsety *= 2;
        }

        //Cono
        else if (_cono)
        {
            offsety += 0.3f;
        }
    }
    #endregion
    #endregion

    #region Coroutines
    public IEnumerator Attack(Animator _animator)
    {
        ///Compruebo que tipo de ataque esta haciendo el personaje y dependiendo de cada uno,
        ///hace una animacion o otra. Además en el puñetazo hace el sonido
        if (_cono) {
            _animator.SetBool("AtaqueCono", true);
            _animator.SetBool("Attack", false);
            yield return new WaitForSeconds(0.2f);
            AudioManager.Instance.Punch();
            _animator.SetBool("AtaqueCono", false);
        }else if(_botella){
          
            _animator.SetFloat("AtaqueX", offsetx);
            _animator.SetFloat("AtaqueY", offsety);
            _animator.SetBool("Attack", true);
            StartCoroutine(_shootComponent.Disparo(new Vector3(offsetx, offsety)));
            yield return new WaitForSeconds(0.2f);
            _animator.SetBool("Attack", false);


        }
        else if (_mejorado)
        {
            _animator.SetBool("AtaqueChapa", true);
            _animator.SetBool("Attack", false);
            yield return new WaitForSeconds(0.2f);
            AudioManager.Instance.Punch();
            _animator.SetBool("AtaqueChapa", false);


        }
        else
        {
            _animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.2f);
            AudioManager.Instance.Punch();
            _animator.SetBool("Attack", false);
        }

        //Comprobación del tipo de ataque para preparar el lanzamiento de la botella o
        //uno físico (Cono, Cubo de chapas, Básico)
        if (!_botella)
        {
            //Activación de la colisión y el render para ver el área de efecto, además de golpear
            Collider2D.enabled = true;

            //Ajuste del ataque a la distancia y dirección correcta si está el ataque mejorado (cubo de chapas)
            if (_mejorado) 
            {
                Collider2D.size = new Vector2( 1f + Mathf.Abs(offsetx) / 2, 1f +  Mathf.Abs(offsety) / 2);
            }
            _spriteRenderer.enabled = true;

            //Ajuste del tamaño del ataque si está el ataque mejorado (cubo de chapas)
            if (_mejorado)
            {
                Collider2D.size = new Vector2(1f + Mathf.Abs(offsetx) / 2, 1f + Mathf.Abs(offsety) / 2);
            }

            //Bucle destinado a esperar un número de FixedUpdates para deshabilitar nuevamente el ataque
            for (int i = duraciondeataque; i > 0; i--)
            {
                yield return new WaitForFixedUpdate();
            }
            //Deshabilitación de las colisiones y render para
            //evitar problemas con otras colisiones o molestias visuales
            Collider2D.enabled = false;
            _spriteRenderer.enabled = false;
            
        
        
        }

        //Comprobación correspondiente al uso del cono o la botella
        if (_cono || _botella)
        {
            //Comprobación para recuperar el tipo de ataque que teniamos
            if (_mejorado)
            {
                //Recuperación del ataque mejorado (Cubo de Chapas) tras el uso del cono
                Chapas();
            }
            else
            {
                //Recuperación del ataque básico tras el uso del cono
                Basico();
            }

            //Desactivación del comprobante del cono y el de la botella
            //al finalizar el ataque
            _cono = false;
            _botella = false;

            //Desactivación del sprite correspondiente al cono o botella en la UI
            UIManager.UiManager.QuitarSpriteCono();
            UIManager.UiManager.QuitarSpriteBotella();

            //Validación para habilitar la recogida de objetos
            _recolector.canBePicked = true;
        }

    }
    #endregion

    void Start()
    {
        //Setting de la comprobación de mejora
        _mejorado = false;

        //Setting de la comprobación del cono
        _cono = false;

        //Setting de la comprobación del botella
        _botella = false;

        //Recogida de los componentes que necesitamos
        Collider2D = GetComponent<BoxCollider2D>();
        _recolector = GetComponentInParent<Recolector_de_Armas>();
        attack = GetComponent<Damage>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shootComponent = GetComponent<ShootComponent>();

        //Deshabilitación de los componentes pertientes para evitar errores al inicio
        _spriteRenderer.enabled = false;
        Collider2D.enabled = false;

        //Setting del ataque basico al inicio
        Basico();
    }

    void LateUpdate()
    {
        //Movemos el objeto a la posición de su padre para evitar que se quede en posiciones extrañas
        //Le añadimos el offset pertinente según la dirección elegida (Calculado en el InputManager/FrankInput)
        transform.position = FrankMovement.Player.transform.position + new Vector3(offsetx, offsety);
    }

}
