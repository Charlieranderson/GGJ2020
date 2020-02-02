using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInputs : PlayerActionSet
{

    public PlayerAction Click;



    public PlayerInputs()
    {
        Click = CreatePlayerAction("Click");
        Click.AddDefaultBinding(Mouse.LeftButton);


    }
}