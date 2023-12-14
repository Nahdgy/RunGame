using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    public GameObject player;

    [SerializeField]
    private Transform playerSpawn;

    private void Start()
    {
        SpawnStartPlayer();
    }
    private void SpawnStartPlayer()
    {
        Instantiate(player, playerSpawn);
    }
}
