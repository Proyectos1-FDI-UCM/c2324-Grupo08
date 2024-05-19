using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTriggerSecreta : MonoBehaviour
{

    #region references
    public Vector3 newCampos;
    public Vector3 newPlayerPos;
    private CameraController _camControl;
    private GameObject _myObject;
    public static BoxCollider2D _transicion;
    public static bool _estadotrans;
    private bool _arriba = true;
    private static GameObject instance;
    public static GameObject Instance { get { return instance; } }
    [SerializeField]
    GameObject SecretRoom;
    bool UpgradeDrop;
    #endregion
    #region method
    public void TransitionAvaible(bool trans)
    {
        if (_transicion != null)
        {
            _transicion.enabled = trans;
        }
    }
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpgradeDrop = false;
        newPlayerPos = new Vector3 (0, -2f,0);
        newCampos = new Vector3(0, -10, 0);
        _camControl = Camera.main.GetComponent<CameraController>();
        _myObject = gameObject;
        _transicion = _myObject.GetComponent<BoxCollider2D>();
        _transicion.enabled = false;
        SecretRoom.transform.position = new Vector3(64f, 0f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null)
        {
            
            if(_arriba == true) {
                StartCoroutine(LevelManager.levelManager.Go2());
                _camControl.minPos += newCampos;
                _camControl.maxPos += newCampos;
                other.transform.position += newPlayerPos;
                
                _transicion.enabled = false;
                _arriba = false;
                _myObject.transform.position = new Vector3(64, -5.1f, 0);
                newPlayerPos = new Vector3(0, 2, 0);
                newCampos = new Vector3(0, 10, 0);
                if (!UpgradeDrop)
                {
                    LevelManager.levelManager.DropUpgrade(SecretRoom);
                    UpgradeDrop = true;
                }
            }
            else if (_arriba == false){
                StartCoroutine(LevelManager.levelManager.Go());
                _camControl.minPos += newCampos;
                _camControl.maxPos += newCampos;
                other.transform.position += newPlayerPos;
                _transicion.enabled = false;
                _arriba = true;
                _myObject.transform.position = new Vector3(64, -4.9f, 0);
                newPlayerPos = new Vector3(0, -2.9f, 0);
                newCampos = new Vector3(0, -10, 0);
            }
            
           }
    }
}
