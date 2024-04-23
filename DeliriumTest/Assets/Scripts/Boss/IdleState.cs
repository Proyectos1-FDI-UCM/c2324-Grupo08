using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        isComplete = false;
    }
    public override void Do()
    {
        trigger.enabled = false;
        isComplete = true;
    }
}
