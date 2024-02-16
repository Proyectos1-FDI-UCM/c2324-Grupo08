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
    public Transform player;
    private Vector3 _targetPos;
    private Vector3 _newPos;
    public Vector3 minPos, maxPos;
    #endregion

    void LateUpdate()
    {
        if (transform.position != player.position)
        {
            _targetPos = player.position;

            Vector3 camEdges = new Vector3(
                Mathf.Clamp(_targetPos.x, minPos.x, maxPos.x),
                Mathf.Clamp(_targetPos.y, minPos.y, maxPos.y),
                Mathf.Clamp(_targetPos.z, minPos.z, maxPos.z));

            _newPos = Vector3.Lerp(transform.position, camEdges, _camVelocity);
            transform.position = _newPos;
        }
    }
}
