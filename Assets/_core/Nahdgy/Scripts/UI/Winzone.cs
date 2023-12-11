using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winzone : MonoBehaviour
{
    [SerializeField]
    private int player;
    [SerializeField]
    private UIManager ui;

    private void Start()
    {
        ui = GameObject.FindAnyObjectByType<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == player)
        {
            ui.Win();
        }
    }

}
