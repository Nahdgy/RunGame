using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilesPrefabs;
    [SerializeField]
    private float zSpawn = 0, tileLenght = 60;

    [SerializeField]
    private int tileCount;
    [SerializeField]
    private Transform playerTransform;
    
    void Start()
    {
        SpawnRandom();    
    }

    void Update()
    {
        
    }
    private void SpawnInfinite()
    {
        if(playerTransform.position.z>zSpawn - (tileCount * tileLenght))
        {
            InstantiateTiles(Random.Range(0, tilesPrefabs.Length));
        }
    }
    private void SpawnRandom()
    {
        for(int i = 0; i < tileCount; i++) 
        {
            if (i == 0)
                InstantiateTiles(0);
            else
                InstantiateTiles(Random.Range(0, tilesPrefabs.Length));
        }
    }
    private void InstantiateTiles(int indexTiles)
    {
        Instantiate(tilesPrefabs[indexTiles], transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLenght;

    }
}
