using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        trigger.enabled = false;
    }
    public override void Exit ()
    {
        trigger.enabled = true;
    }
}