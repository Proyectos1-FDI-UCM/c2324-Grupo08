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
        newPlayerPos = new Vector3 (0, -2f,0);
        newCampos = new Vector3(0, -10, 0);
        _camControl = Camera.main.GetComponent<CameraController>();
        _myObject = gameObject;
        _transicion = _myObject.GetComponent<BoxCollider2D>();
        _transicion.enabled = false;
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
                _myObject.transform.position = new Vector3(80, -5.1f, 0);
                newPlayerPos = new Vector3(0, 2, 0);
                newCampos = new Vector3(0, 10, 0);
            }
            else if (_arriba == false){
                StartCoroutine(LevelManager.levelManager.Go());
                _camControl.minPos += newCampos;
                _camControl.maxPos += newCampos;
                other.transform.position += newPlayerPos;
                _transicion.enabled = false;
                _arriba = true;
                _myObject.transform.position = new Vector3(80, -4.9f, 0);
                newPlayerPos = new Vector3(0, -2.9f, 0);
                newCampos = new Vector3(0, -10, 0);
            }
            
           }
    }
}
