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

    //El n�mero de FixedUpdates que esperar� la corutina
    //antes de deshabilitar el ataque
    [SerializeField] int duraciondeataque;

    //Floats que contienen las componentes del Vector Offset
    //que marca la direcci�n del ataque:
    float offsetx;
    float offsety;

    //Comprobaci�n de los tipos de ataque:
    //Mejora (Cubo de Chapas)
    bool _mejorado;
    //Cono
    bool _cono;
    //Botella
    bool _botella;
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
        }

        public void Basico()
        {
            //Cambio de propiedades acordes al ataque por defecto (B�sico)
            attack.Attack = 2; //Da�o realizado
        }

        public void Cono()
        {
            //Activaci�n del comprobante del uso del cono
            _cono = true;

            //Cambio de propiedades acordes al cono
            attack.Attack = 4;//Da�o realizado
        }
        public void Botella() 
        {
            //Activaci�n del comprobante de la botella
            _botella = true;

            //Cambio de propiedades acordes al cono
            attack.Attack = 3;//Da�o realizado
        }
        #endregion

        //M�todos que asignan el valor de la direcci�n de ataque
        #region Set direction of attack

        public void Setoffsetx(float value)
        {
            offsetx = value;
        }

        public void Setoffsety(float value)
        {
            offsety = value;
        }
        #endregion
    #endregion

    #region Coroutines
    public IEnumerator Attack(Animator _animator)
    {
        bool basico = !_mejorado && !_cono && !_botella;

        //Inicio de la animaci�n de ataque
        if (basico) _animator.SetBool("Attack", true);

        //Retardo para permitir cuadrar el frame de golpe con la activaci�n de la colisi�n 
        yield return new WaitForSeconds(0.2f);

        //Comprobaci�n del tipo de ataque para preparar el lanzamiento de la botella
        //uno f�sico (Cono, Cubo de chapas, B�sico)
        if (!_botella)
        {
            //Activaci�n de la colisi�n y el render para ver el �rea de efecto, adem�s de golpear
            Collider2D.enabled = true;
            _spriteRenderer.enabled = true;

            //Se ejecuta el efecto de sonido
            if (basico) AudioManager.Instance.Punch();

            //Bucle destinado a esperar un n�mero de FixedUpdates para deshabilitar nuevamente el ataque
            for (int i = duraciondeataque; i > 0; i--) yield return new WaitForFixedUpdate();

            //Fin de la animaci�n de ataque
            if(basico) _animator.SetBool("Attack", false);

            //Deshabilitaci�n de las colisiones y render para
            //evitar problemas con otras colisiones o molestias visuales
            Collider2D.enabled = false;
            _spriteRenderer.enabled = false;
        }
        else 
        {
            StartCoroutine(_shootComponent.Disparo(new Vector3(offsetx, offsety)));
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
