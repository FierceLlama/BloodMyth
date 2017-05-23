using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
    {

    public GameObject particleFire;
    public float rate = 10.0f;
    float timesSinceLastSpawn = 0.0f;
    PolygonCollider2D armCollider;


    // Use this for initialization
    void Start()
        {
        this.armCollider = this.gameObject.GetComponent<PolygonCollider2D>();
        }

    // Update is called once per frame
    void Update()
        {
        this.timesSinceLastSpawn += Time.deltaTime;
        float correcttimeBetweenSpawns = 1.0f / this.rate;


        while(this.timesSinceLastSpawn > correcttimeBetweenSpawns)
            {
            this.SpawnFireOnArms();
            this.timesSinceLastSpawn -= correcttimeBetweenSpawns;
            }

        }
    void SpawnFireOnArms()
        {
        int pathIndex = Random.Range(0, this.armCollider.pathCount);
        Vector2[] points = this.armCollider.GetPath(pathIndex);
        int point = Random.Range(0, points.Length);
        Vector2 spawnPoint = Vector2.Lerp(points[point], points[(point + 1) % points.Length], Random.Range(0.0f, 1.0f));
        this.SpawnFire(spawnPoint + (Vector2)this.gameObject.transform.position);
        }

    void SpawnFire(Vector2 inPos)
        {
        Instantiate(this.particleFire, inPos, Quaternion.identity);
        }
    }
