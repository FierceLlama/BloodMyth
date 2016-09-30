using UnityEngine;
using System.Collections;

public class CenterCam : MonoBehaviour {

	public GameObject player;
	public float speed;

	Vector3 curPos;
	Vector3 playerPos;
	
    void Start()
    {
        
    }

	void Update () 
	{
		curPos = Camera.main.transform.position;
		playerPos = player.transform.position;
        playerPos.y += 7;
		playerPos.z = -10;

		transform.position = Vector3.Lerp(curPos, playerPos, speed);
	}
}
