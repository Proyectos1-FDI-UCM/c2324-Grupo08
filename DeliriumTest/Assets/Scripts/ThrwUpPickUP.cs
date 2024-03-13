using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowUpPickUP : MonoBehaviour
{
    #region references
    private VomitComponent _vomit;
    #endregion
    #region parameters
    [SerializeField] private int substractAmount;
    public Slider _vomitBar;
    #endregion
    private void OnTriggerStay2D(Collider2D collision)
    {
        FrankMovement player = collision.gameObject.GetComponent<FrankMovement>();
        if (player != null)
        {
            if (_vomitBar.value != 0)
            {
                SubstractVomit();
                Destroy(this.gameObject);
            }
        }
    }

    /*public void RegisterVomit(Slider vomit)
    {
        Debug.Log("Registrado");
        _vomitBar = vomit;
    }*/
    private void Start()
    {
        _vomit = GetComponent<VomitComponent>();
    }


    private void Awake()
    {

    }
    private void SubstractVomit()
    {
        _vomitBar.value -= substractAmount;
    }
}
