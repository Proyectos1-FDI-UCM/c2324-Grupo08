using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoArrowController : MonoBehaviour
{
    private void Awake()
    {
        LevelManager.RegisterArroy(gameObject);
    }
}
