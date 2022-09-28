using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStart : MonoBehaviour
{
    void Awake()
    {
        //DEFAULT SETTINGS --- RUNS ONLY ONCE

        if (PlayerPrefs.GetString("OyunDahaOnceAcildimi") != "EVET")
        {
            PlayerPrefs.SetFloat("musicvolume", 0.6f);
            PlayerPrefs.SetString("OyunDahaOnceAcildimi", "EVET");
        }
    }
}
