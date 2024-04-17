using UnityEngine;

public class InputTropiezo : InputManager
{
    // Start is called before the first frame update
    #region references
    MovementTropiezo Movement;
    public bool falling = false;
    #endregion

    void Start()
    {
        _animator = GetComponent<Animator>();
        Movement = GetComponent<MovementTropiezo>();
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        if (_playerAttack == null) Debug.LogError("Frank no tiene un ataque puesto. Revisa la escena de Janime para ver un ejemplo de implementación.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Dialogmanager.Instance.StopAllCoroutines();
            Movement.interact();
        }
           
        if (!_canAttack) { 
            _time += Time.deltaTime; 
        }
        if (_time >= _cooldown) { 
            _time = 0; 
            _canAttack = true; 
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
        
            Movement.Dash();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _playerAttack.Setoffsetx(0);

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                finaloffset = -offsety;
                if (_playerAttack._cono)
                {
                    _animator.SetFloat("ConoX", 0);
                    _animator.SetFloat("ConoY", -1);
                }else if (_playerAttack._mejorado)
                {
                    
                    _animator.SetFloat("AtaqueChapaX", 0);
                    _animator.SetFloat("AtaqueChapaY", -1);
                }
                else
                {
                    _animator.SetFloat("AtaqueX", 0);
                    _animator.SetFloat("AtaqueY", -1);
                }
            }
            else
            {
                finaloffset = offsety;

                if (_playerAttack._cono)
                {
                    _animator.SetFloat("ConoX", 0);
                    _animator.SetFloat("ConoY", 1);
                }
                else if (_playerAttack._mejorado)
                {
                    _animator.SetFloat("AtaqueChapaX", 0);
                    _animator.SetFloat("AtaqueChapaY", 1);
                }
                else
                {
                    _animator.SetFloat("AtaqueX", 0);
                    _animator.SetFloat("AtaqueY", 1);
                }
            }
            _playerAttack.Setoffsety(finaloffset);

            if (_canAttack)
            {
                _animator.SetBool("Rascadita", false);
                StartCoroutine(_playerAttack.Attack(_animator));
                _canAttack = false;
            }
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _playerAttack.Setoffsety(0);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                finaloffset = -offsetx;
                if (_playerAttack._cono)
                {
                    _animator.SetFloat("ConoX", -1);
                    _animator.SetFloat("ConoY", 0);
                }
                else if (_playerAttack._mejorado)
                {
                    _animator.SetFloat("AtaqueChapaX", -1);
                    _animator.SetFloat("AtaqueChapaY", 0);
                }
                else
                {
                    _animator.SetFloat("AtaqueX", -1);
                    _animator.SetFloat("AtaqueY", 0);
                }
            }

            else
            {
                finaloffset = offsetx;
                if (_playerAttack._cono)
                {
                    _animator.SetFloat("ConoX", 1);
                    _animator.SetFloat("ConoY", 0);
                }
                else if (_playerAttack._mejorado)
                {
                    _animator.SetFloat("AtaqueChapaX", 1);
                    _animator.SetFloat("AtaqueChapaY", 0);
                }
                else
                {
                    _animator.SetFloat("AtaqueX", 1);
                    _animator.SetFloat("AtaqueY", 0);
                }

            }
            _playerAttack.Setoffsetx(finaloffset);
            
            if (_canAttack)
            {
                _animator.SetBool("Rascadita", false);
                StartCoroutine(_playerAttack.Attack(_animator));
                _canAttack = false;
            }
        }
        Movement.RegisterX(Input.GetAxisRaw("Horizontal"));
        Movement.RegisterY(Input.GetAxisRaw("Vertical"));

        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D)))
        {
            StartCoroutine(Movement.Tropiezo());
            falling = true;
        }

    }

}
