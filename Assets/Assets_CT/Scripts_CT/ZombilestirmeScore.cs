using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombilestirmeScore : MonoBehaviour
{
    private int score;
    [SerializeField] TextMeshProUGUI scoreValueText;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ScoreArttır(bool value)
    {
        if(value){}
            score++;
        scoreValueText.text = score.ToString();
        PlayerPrefs.SetInt("MaxScore", score);
    }
}
