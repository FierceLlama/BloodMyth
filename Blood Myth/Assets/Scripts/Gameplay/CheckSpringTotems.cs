using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpringTotems : MonoBehaviour
    {
    public GameObject[] totems;

    private void Awake()
        {
        int totemMask = IOSystem.Instance.GetLevelTotems();

        foreach (GameObject totem in totems)
            {
            int totemEnum = (int)totem.GetComponent<TotemState>().GetTotemEnum();
            int mask = totemMask & totemEnum;
            if (mask == totemEnum)
                {
                //Debug.Log("Totem visited");
                totem.GetComponent<TotemState>().TotemVisited();
                }
            else
                {
                //Debug.Log("Totem not visited");
                }
            }
        }
    }