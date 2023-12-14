using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreUIObject;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private int scoreDisplay = 0;
    private int layerPlayer = 7;

    private void FixedUpdate()
    {
        scoreText.text = scoreDisplay.ToString();
    }
    private void Start()
    {
        scoreUIObject = GameObject.FindGameObjectWithTag("Score");
        scoreText = scoreUIObject.GetComponent<Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layerPlayer)
        {
            scoreDisplay += 140;

        }
    }

  
}
