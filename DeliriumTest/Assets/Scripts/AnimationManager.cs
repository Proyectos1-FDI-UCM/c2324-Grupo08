using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region references
    private FrankMovement _frankMovement;
    private PlayerAttack _playerAttack;
    private Animator _animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _frankMovement = GetComponent<FrankMovement>();
        _playerAttack = GetComponentInChildren<PlayerAttack>();
    }

    void Idle()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
