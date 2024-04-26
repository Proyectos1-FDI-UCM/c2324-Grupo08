using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        _animation.SetBool("IdleState", true);
        _animation.SetBool("AttackState", false);
        _animation.SetBool("DashState", false);
        _animation.SetBool("EscapingState", false);
    }
        
    
    public override void Do()
    {
        isComplete = true;
    }
}
