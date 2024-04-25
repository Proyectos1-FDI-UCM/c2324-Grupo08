using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform creditTransform;
    void Start()
    {
        creditTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = creditTransform.position + (Vector3.up * speed * Time.deltaTime);
    }
}
