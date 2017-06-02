using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour
    {
    Vector2 velocity;
    public Vector2 minVelocity = new Vector2(-1f,1f);
    public Vector2 maxVelocity = new Vector2(2f,4f);
    public float lifeSpan = 2.0f;
    // Use this for initialization
    void Start()
        {
        this.velocity = new Vector2(Random.Range(minVelocity.x,maxVelocity.x), Random.Range(minVelocity.y, maxVelocity.y));
        }

    // Update is called once per frame
    void Update()
        {
        this.lifeSpan -= Time.deltaTime;
        if(this.lifeSpan <= 0.0f)
            {
            Destroy(this.gameObject);
            }
        this.transform.Translate(velocity * Time.deltaTime);
        }
    }
