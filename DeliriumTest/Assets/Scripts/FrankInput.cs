using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region references
    private FrankMovement _frankMovement;
    private PlayerAttack _playerAttack;
    private Animator _animator;

    #endregion
    #region properties
    [SerializeField]
    float offsetx = 0.7f;
    [SerializeField]
    float offsety = 0.7f;
    float finaloffset;
    [SerializeField]
    float _cooldown = 0.24f;
    float _time;
    bool _canAttack;
    #endregion

    void Start()
    {
        _animator = GetComponent<Animator>();
        _frankMovement = GetComponent<FrankMovement>();
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        if (_playerAttack == null) Debug.LogError("Frank no tiene un ataque puesto. Revisa la escena de Janime para ver un ejemplo de implementación.");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canAttack) { _time += Time.deltaTime; }
        if (_time >= _cooldown) { _time = 0; _canAttack = true; }
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _frankMovement.Dash();
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _animator.SetBool("Rascadita", false);
            _playerAttack.Setoffsetx(0);
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                finaloffset = -offsety;
            }
            else
            {

                finaloffset = offsety;
            }
            _playerAttack.Setoffsety(finaloffset);
            _animator.SetFloat("AtaqueY", finaloffset);


            if (_canAttack)
            {
                StartCoroutine(_playerAttack.Attack(_animator));
                _canAttack = false;
            }
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetBool("Rascadita", false);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                finaloffset = -offsetx;
            }

            else {
                

                finaloffset = offsetx; 
            }
            _playerAttack.Setoffsetx(finaloffset);
            _animator.SetFloat("AtaqueX", finaloffset);
            _playerAttack.Setoffsety(0);
            if (_canAttack)
            {
                StartCoroutine(_playerAttack.Attack(_animator));
                _canAttack = false;
            }
        }
        _frankMovement.RegisterX(Input.GetAxisRaw("Horizontal"));
        _frankMovement.RegisterY(Input.GetAxisRaw("Vertical"));
    }
   
}
