using UnityEngine;

/*
 * --------------------------------------------------
 * | AVISO: Esta clase es una extensión de la clase |
 * | InputManager para implementar un movimiento    |
 * | que de una mayor sensacion de borrachera y     |
 * | la posibilidad de interactuar con objetos.     |
 * | Esto se verá reflejado en el tropiezo y        |
 * | el caminar torcido de Paco.                    |
 * --------------------------------------------------
 */

public class InputTropiezo : InputManager
{
    #region References
    /// <summary>
    /// Referencia al Movement de Paco.
    /// </summary>
    MovementTropiezo Movement;
    #endregion

    #region Properties
    /// <summary>
    /// Comprobante de si nos hemos tropezado o no.
    /// </summary>
    public bool falling = false;
    #endregion

    void Start()
    {
        _animator = GetComponent<Animator>();
        Movement = GetComponent<MovementTropiezo>();
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        if (_playerAttack == null) Debug.LogError("Frank no tiene un ataque puesto.");
    }

    void Update()
    {
        //Si tenemos un Objeto Interactuable detemos el texto que se estaba
        //escribiendo una vez se alla completado y comenzamos a escribir el nuevo dialogo.
        if (Input.GetKeyDown(KeyCode.E) && Dialogmanager.Instance.endText) 
        {
            Dialogmanager.Instance.StopAllCoroutines();
            Movement.interact();
        }
        
        //Calculamos el cooldown del ataque
        if (!_canAttack) { 
            _time += Time.deltaTime; 
        }
        //Reiniciamos el cooldown del ataque y lo activamos
        if (_time >= _cooldown) { 
            _time = 0; 
            _canAttack = true; 
        }
        
        //Activamos el dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Movement.Dash();
        }

        //Comprobamos en que dirección atacamos y con ello mandamos el ataque a esa
        //dirección y activamos las animaciones pertinentes. 
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
            
            //Animacion de ataque
            if (_canAttack)
            {
                _animator.SetBool("Rascadita", false);
                StartCoroutine(_playerAttack.Attack(_animator));
                _canAttack = false;
            }
        }

        //Mandamos la direccion a la que se va a mover Paco al MovementTropiezo.
        Movement.RegisterX(Input.GetAxisRaw("Horizontal"));
        Movement.RegisterY(Input.GetAxisRaw("Vertical"));

        //Marcamos que nos caeremos si hemos soltado el movimiento y estamos tocando ninguna otra dirección.
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D)) 
            && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D)))
        {
            StartCoroutine(Movement.Tropiezo());
            falling = true;
        }

    }

}
