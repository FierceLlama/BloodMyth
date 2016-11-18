using UnityEngine;
using System.Collections;

public class TotemBase : MonoBehaviour {

    public Color totemColor;
    public string power;
    public int Uses;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !col.gameObject.GetComponent<TotemPower>().hasTotemPower && Uses >= 1)
        {
            Uses -= 1;
            col.gameObject.GetComponent<TotemPower>().hasTotemPower = true;
            col.gameObject.GetComponent<TotemPower>().TotemHalo.startColor = totemColor;
            col.gameObject.GetComponent<TotemPower>().curPower = power;
        }
    }
}
