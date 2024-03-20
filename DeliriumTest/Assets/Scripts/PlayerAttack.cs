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

    //El número de FixedUpdates que esperará la corutina
    //antes de deshabilitar el ataque
    [SerializeField] int duraciondeataque;

    //Floats que contienen las componentes del Vector Offset
    //que marca la dirección del ataque:
    float offsetx;
    float offsety;

    //Comprobación de los tipos de ataque:
    //Mejora (Cubo de Chapas)
    bool _mejorado;
    //Cono
    bool _cono;
    //Botella
    bool _botella;
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
        }

        public void Basico()
        {
            //Cambio de propiedades acordes al ataque por defecto (Básico)
            attack.Attack = 2; //Daño realizado
        }

        public void Cono()
        {
            //Activación del comprobante del uso del cono
            _cono = true;

            //Cambio de propiedades acordes al cono
            attack.Attack = 4;//Daño realizado
        }
        public void Botella() 
        {
            //Activación del comprobante de la botella
            _botella = true;

            //Cambio de propiedades acordes al cono
            attack.Attack = 3;//Daño realizado
        }
        #endregion

        //Métodos que asignan el valor de la dirección de ataque
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

        //Inicio de la animación de ataque
        if (basico) _animator.SetBool("Attack", true);

        //Retardo para permitir cuadrar el frame de golpe con la activación de la colisión 
        yield return new WaitForSeconds(0.2f);

        //Comprobación del tipo de ataque para preparar el lanzamiento de la botella
        //uno físico (Cono, Cubo de chapas, Básico)
        if (!_botella)
        {
            //Activación de la colisión y el render para ver el área de efecto, además de golpear
            Collider2D.enabled = true;
            _spriteRenderer.enabled = true;

            //Se ejecuta el efecto de sonido
            if (basico) AudioManager.Instance.Punch();

            //Bucle destinado a esperar un número de FixedUpdates para deshabilitar nuevamente el ataque
            for (int i = duraciondeataque; i > 0; i--) yield return new WaitForFixedUpdate();

            //Fin de la animación de ataque
            if(basico) _animator.SetBool("Attack", false);

            //Deshabilitación de las colisiones y render para
            //evitar problemas con otras colisiones o molestias visuales
            Collider2D.enabled = false;
            _spriteRenderer.enabled = false;
        }
        else 
        {
            StartCoroutine(_shootComponent.Disparo(new Vector3(offsetx, offsety)));
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
