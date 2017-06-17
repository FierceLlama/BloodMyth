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
        //this.coldLH.SetActive(false);
        //this.coldRH.SetActive(false);
        //this.hotLH.SetActive(false);
        //this.hotRH.SetActive(false);
        //this.dehydrated.SetActive(false);
        this.ClearTempFX();
        this.ClearDehydrationFX();
        }

    void Update()
        {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //    SetHotFX();
        //    }

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //    SetColdFX();
        //    }
        }

    public void SetColdFX()
        {
        this.coldLH.SetActive(true);
        this.coldRH.SetActive(true);
        this.hotLH.SetActive(false);
        this.hotRH.SetActive(false);
        }

    public void SetHotFX()
        {
        this.coldLH.SetActive(false);
        this.coldRH.SetActive(false);
        this.hotLH.SetActive(true);
        this.hotRH.SetActive(true);
        }

    public void SetDehydrationFX()
        {
        this.dehydrated.SetActive(true);
        }

    public void ClearTempFX()
        {
        this.coldLH.SetActive(false);
        this.coldRH.SetActive(false);
        this.hotLH.SetActive(false);
        this.hotRH.SetActive(false);
        }

    public void ClearDehydrationFX()
        {
        this.dehydrated.SetActive(false);
        }
    }