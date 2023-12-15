using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textScore;
    [SerializeField]
    private int scoreDisplay = 0;

    private void Update()
    {
        textScore.text = scoreDisplay.ToString();
    }
    public void AddScore(int points)
    {
        scoreDisplay += points;
    }
}
