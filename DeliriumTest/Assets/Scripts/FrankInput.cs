using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region references
    private FrankMovement _frankMovement;
    #endregion
    void Start()
    {
        _frankMovement = GetComponent<FrankMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _frankMovement.Dash();
        }
        
        _frankMovement.RegisterX(Input.GetAxisRaw("Horizontal"));
        _frankMovement.RegisterY(Input.GetAxisRaw("Vertical"));
    }
}
