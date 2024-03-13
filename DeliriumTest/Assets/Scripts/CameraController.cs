using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _camVelocity;
    public float cameraHorizontalBounds;
    private Camera _camera;
    public float leftCamBound;
    public float rightCamBound;
    #endregion
    #region references
    private Transform _player;
    private Vector3 _targetPos;
    private Vector3 _newPos;
    public Vector3 minPos, maxPos;
    #endregion
    private void Start()
    {
        _camera = Camera.main;      
        CalculateCameraBounds(); 
        _player = FrankMovement.Player.gameObject.GetComponent<Transform>();
    }

    private void CalculateCameraBounds()
    {
        float cameraWidth = _camera.aspect * _camera.orthographicSize;
        float CamPosx = _camera.transform.position.x;
        leftCamBound = CamPosx - cameraWidth/2;
        rightCamBound = CamPosx + cameraWidth/2;
        
    }
    void LateUpdate()
    {
        if (transform.position != _player.position)
        {
            _targetPos = _player.position;

            Vector3 camEdges = new Vector3(
                Mathf.Clamp(_targetPos.x, minPos.x, maxPos.x),
                Mathf.Clamp(_targetPos.y, minPos.y, maxPos.y),
                Mathf.Clamp(_targetPos.z, minPos.z, maxPos.z));

            _newPos = Vector3.Lerp(transform.position, camEdges, _camVelocity * Time.deltaTime);
            transform.position = _newPos;
            CalculateCameraBounds ();
        }
    }

    
}
