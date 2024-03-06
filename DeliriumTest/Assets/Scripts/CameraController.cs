using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _camVelocity;
    #endregion
    #region references
    private Transform _player;
    private Vector3 _targetPos;
    private Vector3 _newPos;
    public Vector3 minPos, maxPos;
    #endregion
    private void Start()
    {
        _player = FrankMovement.Player.gameObject.GetComponent<Transform>();
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
        }
    }
}
