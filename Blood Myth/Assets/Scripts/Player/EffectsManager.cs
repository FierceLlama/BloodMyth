using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
    {
    public GameObject coldLH;
    public GameObject coldRH;
    public GameObject hotLH;
    public GameObject hotRH;
    public GameObject dehydrated;

    void Start()
        {
        this.coldLH.enabled = false;
        this.coldRH.enabled = false;
        this.hotLH.enabled = false;
        this.hotRH.enabled = false;
        this.dehydrated.enabled = false;
        }

    void Update()
        {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            {
            SetHot();
            }

        if (Input.GetKeyDown(KeyCode.Alpha2))
            {
            SetCold();
            }
        }

    public void SetCold()
        {
        this.hot.Pause();
        this.cold.Play();
        }

    public void SetHot()
        {
        this.cold.Pause();
        this.hot.Play();
        }
    }