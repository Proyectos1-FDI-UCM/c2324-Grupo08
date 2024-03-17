using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    #region references
    public Vector3 newCampos;
    public Vector3 newPlayerPos;
    private CameraController _camControl;
    private GameObject _myObject;
    public static BoxCollider2D _transicion;
    public static bool _estadotrans;
    public Transform _rightBound;
    #endregion
    #region method
    public static void TransitionAvaible(bool trans)
    {
        _transicion.enabled = trans;
    }
    #endregion
    void Start()
    {
        _camControl = Camera.main.GetComponent<CameraController>();
        _myObject = gameObject;
        _transicion = _myObject.GetComponent<BoxCollider2D>();
        _transicion.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null)
        {
            _camControl.minPos += newCampos;
            _camControl.maxPos += newCampos;
            other.transform.position += newPlayerPos;
            _transicion.enabled = false;
            transform.Translate(Vector2.right * 16);
            _rightBound.position = new Vector3(_rightBound.position.x + 16, _rightBound.position.y, _rightBound.position.z);
        }
    }
}

