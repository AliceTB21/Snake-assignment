using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int[,] grid;
    [SerializeField] private List<Pickup> randomSpawns;
    [SerializeField] private List<Pickup> food;

    [SerializeField] private float minItemSpawnTimer = 8f;
    [SerializeField] private float maxItemSpawnTimer = 15f;
    private float itemSpawnTimer;
    private float timer;
    bool startFoodSpawned = false;


    private void Awake()
    {
        instance = this;
        grid = new int[gridSize.x, gridSize.y];

        InitializeGrid();
        RandomTimer();
    }


    public int[,] GetGrid { get { return grid; } }

    private void InitializeGrid() // Instantiates a tile and sets it to the position on the grid
    {
        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                GameObject newTile = Instantiate(tilePrefab, transform);

                newTile.transform.position = new Vector2(x, y);
            }
        }

        transform.position = new Vector2(-gridSize.y / 2 + 1 / 2, -gridSize.x / 2 - 1 / 2);
    }

    private void Update()
    {
        if(Time.time > timer) // Triggers the RandomSpawn function once time surpasses timer
        {
            timer = Time.time + itemSpawnTimer;

            RandomSpawn();
        }
        if(!startFoodSpawned)
        {
            startFoodSpawned = true;
            SpawnFood();
        }

    }

    public Vector3 GetMinPosition() // Gets the minimum x and y position
    {
        return new Vector3(-gridSize.x / 2, -gridSize.y / 2) * 1;
    }
    public Vector3 GetMaxPosition() // Gets the maximum x and y position
    {
        return new Vector3(gridSize.x / 2, gridSize.y / 2) * 1;
    }

    private void RandomSpawn() // Spawns a random object at a random location
    {
        RandomTimer();
        SpawnItem(randomSpawns);
    }

    private void SpawnItem(List<Pickup> itemList) // Handles spawning item at random grid position
    {
        int item = Random.Range(0, itemList.Count - 1);

        int posX = (int)Random.Range(GetMinPosition().x - 1, GetMaxPosition().x + 1);
        int posY = (int)Random.Range(GetMinPosition().y - 1, GetMaxPosition().y + 1);

        Collider2D hitCollider = Physics2D.OverlapCircle(new Vector2(posX, posY), 0.5f);
        if (hitCollider)
        {
            Tile tile = hitCollider.GetComponent<Tile>();
            if (tile)
            {
                if (tile.heldObject == null)
                {
                    Pickup spawnedItem = Instantiate(itemList[item], new Vector3(posX, posY), Quaternion.identity);
                    tile.heldObject = spawnedItem;
                    SnakeGameManager.Instance.AddItem(spawnedItem.gameObject);
                    Debug.Log("Spawned item");
                    return;
                }
                else
                {
                    Debug.Log("Item already exists");
                     // If it hits a tile with a item it repeats until it can spawn
                }
            }
        }
        else
        {
            Debug.Log("Hit nothing");
            hitCollider = null;
        }
        SpawnItem(itemList);
        Debug.Log("Failed to spawn item");
    }

    public void SpawnFood()
    {
        SpawnItem(food);
        Debug.Log("This food!");
    }

    public void RandomTimer() // Gives a random float between min and max
    {
        itemSpawnTimer = Random.Range(minItemSpawnTimer, maxItemSpawnTimer);
    }

}
