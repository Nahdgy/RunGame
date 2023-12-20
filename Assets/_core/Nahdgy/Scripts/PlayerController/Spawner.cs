using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    private GameObject _instantiatedPlayer;


    [SerializeField]
    private Transform playerSpawn;

    private void Start()
    {
        SpawnStartPlayer();
    }
    private void SpawnStartPlayer()
    {
        _instantiatedPlayer = Instantiate(player, playerSpawn);
        _instantiatedPlayer.transform.SetParent(null);
    }
}
