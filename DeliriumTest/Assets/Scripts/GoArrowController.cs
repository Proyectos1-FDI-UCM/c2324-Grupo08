using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoArrowController : MonoBehaviour
{
    void Awake()
    {
        LevelManager.levelManager.RegisterArroy(this);
    }
    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }
}
