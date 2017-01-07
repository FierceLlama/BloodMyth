using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetComboLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }
}