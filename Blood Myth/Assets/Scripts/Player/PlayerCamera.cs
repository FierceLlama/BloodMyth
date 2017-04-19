using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
	public GameObject player;
	public float speed;
    public float offsetY;
    public float offsetZ;
    public float offsetX;
    public float smooth;
    private Player _playerScript;

	Vector3 curPos;
	Vector3 playerPos;

    //public bool isZooming = false;
    void Start()
    {
        this._playerScript = player.GetComponent<Player>();
    }

	void FixedUpdate () 
	{
		curPos = Camera.main.transform.position;
		playerPos = player.transform.position;
        playerPos.y += offsetY;
        playerPos.z -= offsetZ;
        // Camera needs to offset in the x based on player facing direction
        if (this._playerScript.IsFacingRight())
        {
            playerPos.x += offsetX;
        }
        else
        {
            playerPos.x -= offsetX; 
        }

		transform.position = Vector3.Lerp(curPos, playerPos, speed * smooth * Time.smoothDeltaTime);
	}
}