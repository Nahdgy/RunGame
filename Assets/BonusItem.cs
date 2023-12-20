using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BonusItem : MonoBehaviour
{
    [SerializeField] private int points;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            FindObjectOfType<Scoring>().AddScore(points);
            other.gameObject.GetComponent<Player>().speed += 0.5f;
            Destroy(gameObject);
        }
    }
}
