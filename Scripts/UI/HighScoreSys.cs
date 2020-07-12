using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreSys : MonoBehaviour
{
    public int highScore;
    public TextMeshProUGUI scoreDis;
    void Start()
    {
        highScore = PlayerPrefs.GetInt("Score");
    }

    private void Update()
    {
        scoreDis.SetText("High Score: " + highScore);
    }
}
