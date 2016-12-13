using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemState : MonoBehaviour
{
    public WorldTotems worldTotem;
    private bool _visited = false;

    public WorldTotems GetTotemEnum()
    {
        return this.worldTotem;
    }

    public void TotemVisited()
    {
        this._visited = true;
    }
}