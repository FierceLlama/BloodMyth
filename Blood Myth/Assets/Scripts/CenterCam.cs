using UnityEngine;
using System.Collections;

public class CenterCam : MonoBehaviour {

	public GameObject player;
	public float speed;
    public float offsetY;
    public float offsetZ;
    public float offsetX;
    public float smooth;

	Vector3 curPos;
	Vector3 playerPos;

    //public bool isZooming = false;
    void Start()
    {
        
    }

	void Update () 
	{
		curPos = Camera.main.transform.position;
		playerPos = player.transform.position;
        playerPos.y += offsetY;
        playerPos.z -= offsetZ;
        // Camera needs to offset in the x based on player facing direction
        if (player.GetComponent<PlayerMovement>().isFacingRight())
        {
            playerPos.x += offsetX;
        }
        else
        {
            playerPos.x -= offsetX;
        }

		transform.position = Vector3.Lerp(curPos, playerPos, speed * smooth * Time.smoothDeltaTime);


        //if (!isZooming && Camera.main.orthographicSize > 14)
        //{
        //    Camera.main.orthographicSize -= 0.1f;
        //}
        
	}

    //Nick Testing Auto-Zoom out
    //*
    //public void AutoZoomIN()
    //{
    //    isZooming = true;
    //    if (Camera.main.orthographicSize < 22)
    //    {
    //        Camera.main.orthographicSize += 0.1f;
    //    }

    //}

    

    //*/
}
