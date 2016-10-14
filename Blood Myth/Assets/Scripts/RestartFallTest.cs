using UnityEngine;
using System.Collections;

public class RestartFallTest : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("FallTest");
        }
    }
}
