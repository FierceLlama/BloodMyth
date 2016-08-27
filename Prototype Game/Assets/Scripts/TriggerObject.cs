using UnityEngine;
using System.Collections;

public class TriggerObject : MonoBehaviour {


    public string triggerPower;

    //public GameObject Before;
    public GameObject After;

    public bool smallObj;
    bool Activated = false;

    public ParticleSystem shrubSys;


    void Update()
    {
        if (triggerPower == GameObject.FindGameObjectWithTag("Player").GetComponent<TotemPower>().curPower)
            shrubSys.Play();
        else
            shrubSys.Stop();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!smallObj)
        {
            if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<TotemPower>().curPower == triggerPower && !Activated)
            {
                col.gameObject.GetComponent<TotemPower>().hasTotemPower = false;
                col.gameObject.GetComponent<TotemPower>().curPower = " ";
                /*
                if (Before.gameObject != null)
                    Before.SetActive(false);
                    //*/
                After.SetActive(true);

                shrubSys.Stop();

                Activated = true;
            }
        }
        else
        {
            if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<TotemPower>().curPower == triggerPower && !Activated)
            {
                /*
                if (Before.gameObject != null)
                    Before.SetActive(false);
                //*/
                col.gameObject.GetComponent<TotemPower>().hasTotemPower = false;
                col.gameObject.GetComponent<TotemPower>().curPower = " ";

                After.SetActive(true);

                shrubSys.Stop();

                Activated = true;
            }
        }
    }
}
