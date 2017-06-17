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
        this.coldLH.SetActive(false);
        this.coldRH.SetActive(false);
        this.hotLH.SetActive(false);
        this.hotRH.SetActive(false);
        this.dehydrated.SetActive(false);
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
        this.coldLH.SetActive(true);
        this.coldRH.SetActive(true);
        this.hotLH.SetActive(false);
        this.hotRH.SetActive(false);
        }

    public void SetHot()
        {
        this.coldLH.SetActive(false);
        this.coldRH.SetActive(false);
        this.hotLH.SetActive(true);
        this.hotRH.SetActive(true);
        }
    }