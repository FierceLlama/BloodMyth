using UnityEngine;
using System.Collections;

public class TempObjectController: MonoBehaviour
{
	public float SpeedSwitch; //Time in between switching on and off
    public string clipName;

	void Start () 
	{
		InvokeRepeating ("SwitchOff", SpeedSwitch, SpeedSwitch);
	}
	
	void SwitchOff()
	{
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            AudioManager.Instance.PlaySound(clipName, AudioType.SFX);
        }
	}
}