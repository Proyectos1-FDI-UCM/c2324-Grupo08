using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    // Este estado est� vaci� para hacer que el boss est� parado por unos segundos
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
