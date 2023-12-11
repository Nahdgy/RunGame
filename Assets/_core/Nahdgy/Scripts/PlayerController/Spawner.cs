using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    public GameObject player;

    [SerializeField]
    private Skinchoose character;
    [SerializeField]
    private Transform playerSpawn;

    private void OnEnable()
    {
        character = GameObject.FindAnyObjectByType<Skinchoose>();
        player = character.playerChosen;
        SpawnStartPlayer();
    }
    private void SpawnStartPlayer()
    {
        Instantiate(player, playerSpawn);
    }
}
