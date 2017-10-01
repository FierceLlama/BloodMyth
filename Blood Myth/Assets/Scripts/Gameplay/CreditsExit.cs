using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsExit : MonoBehaviour
    {
    public GameObject creditsScreen;

    void Start()
        {
        this.DisableCredits();
        }

    public void EnableCredits()
        {
        this.creditsScreen.SetActive(true);
        }

    public void DisableCredits()
        {
        this.creditsScreen.SetActive(false);
        }
    }