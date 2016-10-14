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
        if (player.GetComponent<Movement>().isFacingRight())
        {
            playerPos.x += offsetX;
        }
        else
        {
            playerPos.x -= offsetX;
        }

		transform.position = Vector3.Lerp(curPos, playerPos, speed * smooth * Time.smoothDeltaTime);

        //Nick Testing Auto-Zoom out
        /*
        if (curPos == playerPos && Camera.main.orthographicSize < 20)
        {
            Camera.main.orthographicSize += 0.1f;
        }

        else if(Camera.main.orthographicSize >15)
        {
            Camera.main.orthographicSize -= 0.1f;
        }
        //*/
	}
}
