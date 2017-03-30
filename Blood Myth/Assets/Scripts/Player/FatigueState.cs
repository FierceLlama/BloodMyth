using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FatigueState 
{
    public abstract void Handle();
    public abstract void RunLeft();
    public abstract void RunRight();
    public abstract void Sprinting();
    public abstract void NotSprinting();
}

public class NormalFatigue : FatigueState
    {
    public override void Handle()
        {
        }
    public override void RunLeft()
        {
        }
    public override void RunRight()
        {
        }
    public override void Sprinting()
        {
        }
    public override void NotSprinting()
        {
        }
    }

public class TiredFatigue : FatigueState
    {
    public override void Handle()
        {
        }
    public override void RunLeft()
        {
        }
    public override void RunRight()
        {
        }
    public override void Sprinting()
        {
        }
    public override void NotSprinting()
        {
        }
    }

public class ExhaustedFatigue : FatigueState
    {
    public override void Handle()
        {
        }
    public override void RunLeft()
        {
        }
    public override void RunRight()
        {
        }
    public override void Sprinting()
        {
        }
    public override void NotSprinting()
        {
        }
    }
