using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FatigueStateBaseClass
    {
    enum States { Normal, Tired, Exhausted}

    public abstract void Enter();

    public abstract void Update();
    }