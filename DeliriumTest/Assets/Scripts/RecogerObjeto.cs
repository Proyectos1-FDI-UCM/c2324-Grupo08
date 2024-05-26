using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecogerObjeto : MonoBehaviour
{
    #region properties
    [SerializeField] private Slider vomitBar;
    [SerializeField] private Animator _popUpAnimator;
    #endregion

    #region parameters
    private float cambioVomito = 0.5f;
    [SerializeField] private float healing;
    [SerializeField] private float ReduceVomit;
    #endregion

    #region references
    [SerializeField] private LifeBarComponenet _lifeBarComponent;
    [SerializeField] private LifeBarComponenet lifebar;
    [SerializeField] private VomitComponent _vomitCuantity;
    [SerializeField] private UIManager _uiManager;
    private Damage _damage;
    private PlayerAttack _playerAttack;
    private InputManager _inputManager;
    private HealthComponent _healthComponent;
    #endregion

    /* 
     * !!IMPORTANTE!!
     * ORDEN DE LOS PICK-UPS:
     * 1 = Bolsa de Patatas
     * 2 = Botella de Agua
     * 3 = Kebab
     * 4 = Bebida Energética
     * 5 = Cubo de Chapas
     */
    /// <summary>
    /// Se obtienen las referencias a los distintos componentes
    /// </summary>

    void Start()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        _damage = GetComponentInChildren<Damage>();
        _inputManager = GetComponent<InputManager>();
        _popUpAnimator.SetInteger("PopUpn", 0);
        if (_uiManager == null)
        {
            Debug.Log(name + ": Falta la referencia al Componente \"UIManager\"");
            _uiManager = UIManager.UiManager;
            if (_uiManager == null)
            {
                GameObject Faltante = new GameObject();
                Faltante.name = "UIManagerFaltante";
                _uiManager = Faltante.AddComponent<UIManager>();
            }
        }
    }

    /// <summary>
    /// El jugador recibe un efecto dependiendo del objeto
    /// </summary>
    public void Recogida(int ObjID, GameObject pickedObj)
    {
        /// <summary>
        /// Al recoger la botella de agua, el vómito del jugador se reduce
        /// </summary>
        if (ObjID == 1) //Lógica correspondiente a la reducción de vómito
        {
            _popUpAnimator.SetInteger("PopUpn", 1);
            vomitBar.value = vomitBar.value - ReduceVomit;
            AudioManager.Instance.AguaPickup();
            Destroy(pickedObj);
            _popUpAnimator.SetInteger("PopUpn", 1);
            StartCoroutine(PopUpEnd());
        }
        /// <summary>
        /// Al recoger la bolsa de patatas, el jugador se cura una cantidad determinada
        /// </summary>
        else if (ObjID == 2) //Lógica correspondiente a la curación
        {
            if (_healthComponent.Health < _healthComponent.MaxHealth)
            {

                _healthComponent.Healing(healing);
                lifebar.DrawHearts();
                AudioManager.Instance.PatataPickup();
                Destroy(pickedObj);
                _popUpAnimator.SetInteger("PopUpn", 2);
                StartCoroutine(PopUpEnd());
            }
        }
        /// <summary>
        /// Al recoger el Kebab, el jugador adquiere un "corazón" más
        /// </summary>
        else if (ObjID == 3) //Lógica correspondiente a la mejora del "Kebab"
        {
            _lifeBarComponent.HealthUP();
            _vomitCuantity._vomitcuantity = cambioVomito;
            Destroy(pickedObj);
            _uiManager.PonerMejora(ObjID);
            _uiManager.PonerIndMejora(ObjID);
        }
        /// <summary>
        /// Al recoger la bebida energética, se podrán hacer dos dash seguidos sin penalización
        /// </summary>
        else if (ObjID == 4) //Lógica correspondiente a la mejora de "La Bebida Energética"
        {
            FrankMovement.Player.GetComponent<FrankMovement>().DashUpgrade();
            Destroy(pickedObj);
            _uiManager.PonerMejora(ObjID);
            _uiManager.PonerIndMejora(ObjID);
        }
        /// <summary>
        /// Al recoger el cubo de chapas, el jugador tendrá un ataque más fuerte y con mayor alcance
        /// </summary>
        else if (ObjID == 5) //Lógica correspondiente a la mejora del "Cubo de Chapas"
        {
            _inputManager.DisableCooldown();
            _playerAttack.Chapas();
            Destroy(pickedObj);
            _uiManager.PonerMejora(ObjID);
            _uiManager.PonerIndMejora(ObjID);
        }

    }

    /// <summary>
    /// Tras una cantidad de tiempo determinada, se deja de mostrar el PopUp de cada efecto
    /// </summary>
    public IEnumerator PopUpEnd()
    {
        yield return new WaitForSeconds(0.6f);
        _popUpAnimator.SetInteger("PopUpn", 0);
    }
}
