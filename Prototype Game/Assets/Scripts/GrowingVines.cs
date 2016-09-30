using UnityEngine;
using System.Collections;

public class GrowingVines : MonoBehaviour {

    public Vector3 ScaleDir;

    public float ScaleRate;

    public GameObject platform;

    void Start()
    {
        
    }

	void Update () 
	{
        if (isActiveAndEnabled)
        {
            platform.GetComponent<PolygonCollider2D>().isTrigger = true;

            if (transform.localScale.y < ScaleDir.y)
                transform.localScale += new Vector3(0, ScaleRate, 0);
        
            if (transform.position.y < 8)
                transform.position += new Vector3(0, ScaleRate, 0);
            else
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                platform.GetComponent<PolygonCollider2D>().isTrigger = false;
            }
        }
	}

}
