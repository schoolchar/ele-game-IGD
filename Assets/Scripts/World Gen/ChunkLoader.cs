using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public Transform player;  // The player transform
    public float tileSize = 1f;  // Size of each tile
    public int radius = 5;  // Radius around the player to build the floor
    public GameObject[] tilePrefabs;  // Array of tile prefabs to choose from

    private Vector3 lastPlayerPosition;
    private System.Random random = new System.Random();
    private Dictionary<Vector2, GameObject> activeTiles = new Dictionary<Vector2, GameObject>();

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        lastPlayerPosition = player.position;
        BuildFloor();
    }

    void Update()
    {
        // Rebuild the floor if the player has moved
        if (Vector3.Distance(player.position, lastPlayerPosition) >= tileSize)
        {
            lastPlayerPosition = player.position;
            BuildFloor();
        }
    }

    void BuildFloor()
    {
        Vector3 playerPosition = player.position;
        HashSet<Vector2> newActiveTiles = new HashSet<Vector2>();

        // Build new tiles around the player
        for (int x = -radius; x <= radius; x++)
        {
            for (int z = -radius; z <= radius; z++)
            {
                Vector3 tilePosition = new Vector3(
                    Mathf.Floor(playerPosition.x / tileSize) * tileSize + x * tileSize,
                    Mathf.Floor(0 / tileSize) * tileSize,
                    Mathf.Floor(playerPosition.z / tileSize) * tileSize + z * tileSize
                );

                Vector2 tileKey = new Vector2(tilePosition.x, tilePosition.z);
                newActiveTiles.Add(tileKey);

                if (!activeTiles.ContainsKey(tileKey))
                {
                    int randomIndex = random.Next(tilePrefabs.Length);
                    GameObject tile = Instantiate(tilePrefabs[randomIndex], tilePosition, Quaternion.identity);
                    tile.transform.parent = transform;
                    activeTiles[tileKey] = tile;
                }
                else
                {
                    activeTiles[tileKey].SetActive(true);
                }
            }
        }

        // Deactivate tiles that are no longer within the radius
        foreach (var tile in activeTiles)
        {
            if (!newActiveTiles.Contains(tile.Key))
            {
                tile.Value.SetActive(false);
            }
        }
    }
}