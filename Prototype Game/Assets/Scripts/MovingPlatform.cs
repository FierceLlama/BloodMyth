using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float Distance;
	public float speed = 1f;

	Vector3 LeftPos = new Vector3(0,0,0);
	Vector3 RightPos = new Vector3(0,0,0);

	bool goingRight = true;

	void Start () 
	{
		LeftPos = transform.position;
		LeftPos.x -= Distance;

		RightPos = transform.position;
		RightPos.x += Distance;
	}

	void Update () 
	{
		if(transform.position.x <= RightPos.x && goingRight)
			transform.position = Vector3.Lerp (transform.position, RightPos, speed * Time.deltaTime);

		else if(transform.position.x >= LeftPos.x && !goingRight)
			transform.position = Vector3.Lerp (transform.position, LeftPos, speed * Time.deltaTime);


		if (transform.position.x >= RightPos.x - 0.5f)
			goingRight = false;
		else if (transform.position.x <= LeftPos.x + 0.5f)
			goingRight = true;
	}
}
