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
    private static CamTriggerSecreta instance;
    public static CamTriggerSecreta Instance { get { return instance; } }
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
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        UpgradeDrop = false;
        transform.position += new Vector3(0f, 0.45f, 0f);
        newPlayerPos = new Vector3 (0, -2f,0);
        newCampos = new Vector3(0, -10, 0);
        _camControl = Camera.main.GetComponent<CameraController>();
        _myObject = gameObject;
        _transicion = _myObject.GetComponent<BoxCollider2D>();
        _transicion.enabled = false;
        SecretRoom = GetComponentInParent<Transform>().gameObject;
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
                _myObject.transform.position += new Vector3(0, -0.55f, 0);
                newPlayerPos = new Vector3(0, 2, 0);
                newCampos = new Vector3(0, 10, 0);
                if (!UpgradeDrop)
                {
                    LevelManager.levelManager.DropUpgrade(SecretRoom.transform.position + new Vector3(0f, -4.5f, 0f));
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
                _myObject.transform.position += new Vector3(0, 0.45f, 0);
                newPlayerPos = new Vector3(0, -2.9f, 0);
                newCampos = new Vector3(0, -10, 0);
            }
            
           }
    }
}
