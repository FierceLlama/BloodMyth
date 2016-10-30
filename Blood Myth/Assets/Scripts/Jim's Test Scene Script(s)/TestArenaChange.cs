using UnityEngine;
using System.Collections;

public class TestArenaChange : MonoBehaviour {

    public GameObject mainCharacter;


    public void ChangeArena(Transform spawnPoint)
    {
        Debug.Log("boop");
        mainCharacter.transform.position = spawnPoint.position;
    }

}
