using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            this.GetComponent<StopBackgroundMusic>().StopMusic();
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }
}