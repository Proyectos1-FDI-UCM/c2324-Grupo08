using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

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

    #region referneces
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
    * 1 = Bolsa de Patatas
    * 2 = Botella de Agua
    * 3 = Kebab
    * 4 = Bebida Energética
    * 5 = Cubo de Chapas
    */
    void Start()
    {
        _healthComponent = GetComponent<HealthComponent>(); 
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        _damage = GetComponentInChildren<Damage>();
        _inputManager = GetComponent<InputManager>();
        _popUpAnimator.SetInteger("PopUpn", 0);
    }

    public void Recogida(int ObjID, GameObject pickedObj)
    {
        if (ObjID == 1) //Lógica correspondiente a la reducción de vómito
        {
            _popUpAnimator.SetInteger("PopUpn", 1);
            vomitBar.value = vomitBar.value - ReduceVomit;
            AudioManager.Instance.AguaPickup();
            Destroy(pickedObj);
            _popUpAnimator.SetInteger("PopUpn", 1);
            StartCoroutine(PopUpEnd());
        }

        else if (ObjID == 2) //Lógica correspondiente a la curación
        {
            if (_healthComponent.Health != _healthComponent.MaxHealth)
            {
                
                _healthComponent.Healing(healing);
                lifebar.DrawHearts();
                AudioManager.Instance.PatataPickup();
                Destroy(pickedObj);
                _popUpAnimator.SetInteger("PopUpn", 2);
                StartCoroutine(PopUpEnd());
            }           
        }
        else if( ObjID == 3) //Lógica correspondiente a la mejora del "Kebab"
        {
            _lifeBarComponent.HealthUP();
            _vomitCuantity._vomitcuantity = cambioVomito;
            Destroy(pickedObj);
            _uiManager.PonerMejora(ObjID);
            _uiManager.PonerIndMejora(ObjID);
        }
        else if (ObjID == 4) //Lógica correspondiente a la mejora de "La Bebida Energética"
        {
            FrankMovement.Player.GetComponent<FrankMovement>().DashUpgrade();
            Destroy(pickedObj);
            _uiManager.PonerMejora(ObjID);
            _uiManager.PonerIndMejora(ObjID);
        }
        else if(ObjID == 5) //Lógica correspondiente a la mejora del "Cubo de Chapas"
        {
            _inputManager.DisableCooldown();
            _playerAttack.Chapas();
            Destroy(pickedObj);
            _uiManager.PonerMejora(ObjID);
            _uiManager.PonerIndMejora(ObjID);
        }

    }
    //Salta un icono de recogida de pickup
    public IEnumerator PopUpEnd()
    {   
        yield return new WaitForSeconds(0.6f);
        _popUpAnimator.SetInteger("PopUpn", 0);
    }
}
