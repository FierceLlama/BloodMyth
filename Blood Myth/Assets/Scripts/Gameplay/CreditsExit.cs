using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsExit : MonoBehaviour
    {
    public GameObject creditsScreen;
    public GameObject controlsScreen;

    void Start()
        {
        this.DisableCredits();
        this.DisableControls();
        }

    public void EnableCredits()
        {
        this.creditsScreen.SetActive(true);
        }

    public void DisableCredits()
        {
        this.creditsScreen.SetActive(false);
        }

    public void EnableControls()
        {
        this.controlsScreen.SetActive(true);
        }

    public void DisableControls()
        {
        this.controlsScreen.SetActive(false);
        }
    }