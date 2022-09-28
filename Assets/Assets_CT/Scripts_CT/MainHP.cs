using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHP : MonoBehaviour
{
    [SerializeField] Slider hpValue;
    [SerializeField] GameObject GameOverMenu;

    void Start()
    {

    }

    void Update()
    {

    }

    public void HpAzalt(float kacHasarAldi, bool isGameOver)
    {
        float guncelCan = hpValue.value;
        guncelCan -= kacHasarAldi;
        hpValue.value = guncelCan;

        if (isGameOver)
        {
            //KARAKTER ÖLDÜ. GAMEOVER
            GameOverMenu.SetActive(true);
        }
    }
}
