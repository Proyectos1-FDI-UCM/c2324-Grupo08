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
    [SerializeField] private Slider _vomitBar;
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
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
        else
        {
            Debug.LogError("Object not found");
        }
    }
    private void Start()
    {
        _vomit = GetComponent<VomitComponent>();
    }
    private void SubstractVomit()
    {
        _vomitBar.value -= substractAmount;
    }
}
