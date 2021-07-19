using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] startPrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnPos = 0;
    private float tileLength = 100;


    [SerializeField] private Transform player;
    private int startTiles = 6;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnTile(i,true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - 60 > spawnPos - (startPrefabs.Length * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length), false);
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex, bool isStart)
    {
        GameObject nextTile;
        if (isStart)
            nextTile = Instantiate(startPrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        else
            nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
