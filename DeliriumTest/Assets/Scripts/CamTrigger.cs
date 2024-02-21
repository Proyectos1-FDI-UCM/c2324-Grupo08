using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    #region references
    public Vector3 newCampos;
    public Vector3 newPlayerPos;
    private CameraController _camControl;
    #endregion

    void Start()
    {
        _camControl = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _camControl.minPos += newCampos;
            _camControl.maxPos += newCampos;
            other.transform.position += newPlayerPos;
        }
    }
}

