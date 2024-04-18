using System.Collections;
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
    /// El n�mero de FixedUpdates que esperar� la corutina
    ///antes de deshabilitar el ataque
    ///</summary>
    [SerializeField] int duraciondeataque;

    /// <summary>
    ///Floats que contienen las componentes del Vector Offset 
    /// que marca la direcci�n del ataque:
    /// </summary>
    private float offsetx;
    private float offsety;

    //Comprobaci�n de los tipos de ataque:
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

    //M�todos que cambian el ataque y sus propiedades
    #region Set type of Attack

    public void Chapas()
    {
        //Activaci�n del comprobante de la mejora (Cubo de Chapas)
        _mejorado = true;

        //Cambio de propiedades acordes al uso de la mejora (Cubo de Chapas)
        attack.Attack = 1; //Da�o realizado
        duraciondeataque = 2; //Duraci�n del ataque
    }

    public void Basico()
    {
        //Cambio de propiedades acordes al ataque por defecto (B�sico)
        attack.Attack = 2; //Da�o realizado
        duraciondeataque = 2; //Duraci�n del ataque
        Collider2D.size = new Vector2(1, 1); //Tama�o por defecto del collider
    }

    public void Cono()
    {
        //Activaci�n del comprobante del uso del cono
        _cono = true;

        //Cambio de propiedades acordes al cono
        attack.Attack = 4; //Da�o realizado
        duraciondeataque = 3; //Duraci�n del ataque
        Collider2D.size = new Vector2(1.5f, 1.5f); //Tama�o del collider para el cono
    }
    public void Botella()
    {
        //Activaci�n del comprobante de la botella
        _botella = true;

        //Cambio de propiedades acordes al botella
        attack.Attack = 3; //Da�o realizado
    }
    #endregion

    //M�todos que asignan el valor de la direcci�n de ataque
    #region Set direction of attack

    public void Setoffsetx(float value)
    {
        //Set b�sico de la distancia del ataque
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
        //Set b�sico de la distancia del ataque
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
        ///hace una animacion o otra. Adem�s en el pu�etazo hace el sonido
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

        //Comprobaci�n del tipo de ataque para preparar el lanzamiento de la botella o
        //uno f�sico (Cono, Cubo de chapas, B�sico)
        if (!_botella)
        {
            //Activaci�n de la colisi�n y el render para ver el �rea de efecto, adem�s de golpear
            Collider2D.enabled = true;

            //Ajuste del ataque a la distancia y direcci�n correcta si est� el ataque mejorado (cubo de chapas)
            if (_mejorado) 
            {
                Collider2D.size = new Vector2( 1f + Mathf.Abs(offsetx) / 2, 1f +  Mathf.Abs(offsety) / 2);
            }
            _spriteRenderer.enabled = true;

            //Ajuste del tama�o del ataque si est� el ataque mejorado (cubo de chapas)
            if (_mejorado)
            {
                Collider2D.size = new Vector2(1f + Mathf.Abs(offsetx) / 2, 1f + Mathf.Abs(offsety) / 2);
            }

            //Bucle destinado a esperar un n�mero de FixedUpdates para deshabilitar nuevamente el ataque
            for (int i = duraciondeataque; i > 0; i--)
            {
                yield return new WaitForFixedUpdate();
            }
            //Deshabilitaci�n de las colisiones y render para
            //evitar problemas con otras colisiones o molestias visuales
            Collider2D.enabled = false;
            _spriteRenderer.enabled = false;
            
        
        
        }

        //Comprobaci�n correspondiente al uso del cono o la botella
        if (_cono || _botella)
        {
            //Comprobaci�n para recuperar el tipo de ataque que teniamos
            if (_mejorado)
            {
                //Recuperaci�n del ataque mejorado (Cubo de Chapas) tras el uso del cono
                Chapas();
            }
            else
            {
                //Recuperaci�n del ataque b�sico tras el uso del cono
                Basico();
            }

            //Desactivaci�n del comprobante del cono y el de la botella
            //al finalizar el ataque
            _cono = false;
            _botella = false;

            //Desactivaci�n del sprite correspondiente al cono o botella en la UI
            UIManager.UiManager.QuitarSpriteCono();
            UIManager.UiManager.QuitarSpriteBotella();

            //Validaci�n para habilitar la recogida de objetos
            _recolector.canBePicked = true;
        }

    }
    #endregion

    void Start()
    {
        //Setting de la comprobaci�n de mejora
        _mejorado = false;

        //Setting de la comprobaci�n del cono
        _cono = false;

        //Setting de la comprobaci�n del botella
        _botella = false;

        //Recogida de los componentes que necesitamos
        Collider2D = GetComponent<BoxCollider2D>();
        _recolector = GetComponentInParent<Recolector_de_Armas>();
        attack = GetComponent<Damage>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shootComponent = GetComponent<ShootComponent>();

        //Deshabilitaci�n de los componentes pertientes para evitar errores al inicio
        _spriteRenderer.enabled = false;
        Collider2D.enabled = false;

        //Setting del ataque basico al inicio
        Basico();
    }

    void LateUpdate()
    {
        //Movemos el objeto a la posici�n de su padre para evitar que se quede en posiciones extra�as
        //Le a�adimos el offset pertinente seg�n la direcci�n elegida (Calculado en el InputManager/FrankInput)
        transform.position = FrankMovement.Player.transform.position + new Vector3(offsetx, offsety);
    }

}
