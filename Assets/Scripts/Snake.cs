using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector3 position;
    [SerializeField] private float timeUntilMovement = 1f;
    private float timer;
    private bool canMove;

    [SerializeField] private GameObject headPrefab;
    [SerializeField] private GameObject bodyPrefab;
    public CLinkedList<Transform> parts = new CLinkedList<Transform>();
    Vector3 pos;
    Vector3 lastPos;

    private Grid grid;

    private void Start()
    {
        canMove = true;
        grid = Grid.instance;
        position = new Vector3(0, 0, 0);
        SnakeGameManager.Instance.AddItem(this.gameObject);

        InstantiateStartSnakeParts();
    }

    private void InstantiateStartSnakeParts() // Spawns the head at start
    {
        GameObject newHead = Instantiate(headPrefab, transform);
        newHead.transform.position = Vector3.zero;
        parts.AddLast(newHead.transform);
    }


    private void Update()
    {
        Movement();
        TileMovement();

        if(Input.GetKeyDown(KeyCode.U))
        {
            AddPart();
        }

    }

    private void TileMovement() // Checks if snake is hitting end of grid and moves the snake to other side of it and handles part movement
    {
        if (Time.time > timer && canMove)
        {
            timer = timeUntilMovement + Time.time;

            for (int i = 0; i < parts.Count; i++) // Moves the all the parts of the snake
            {
                if (i == 0)
                {
                    pos = parts.GetAtIndex(i).position;
                    if (parts.Count == 1)
                        lastPos = pos;

                    parts.GetAtIndex(i).GetComponent<SnakeMovement>().MoveHead(position);


                }
                if (i != 0)
                {
                    if (i == parts.Count - 1 && parts.Count > 1)
                    {
                        lastPos = parts.GetAtIndex(i).position;
                    }
                    Vector3 tempPos = parts.GetAtIndex(i).position;
                    parts.GetAtIndex(i).GetComponent<SnakeMovement>().MoveBody(pos);
                    pos = tempPos;
                }


            }
            CheckBorders();
        }
    }

    private void CheckBorders() // Checks if head hits a edge
    {
        if (parts.GetAtIndex(0).position.y < grid.GetMinPosition().y)
        {
            parts.GetAtIndex(0).position = new Vector3(parts.GetAtIndex(0).position.x, grid.GetMaxPosition().y);
        }
        else if (parts.GetAtIndex(0).position.y > grid.GetMaxPosition().y)
        {
            parts.GetAtIndex(0).position = new Vector3(parts.GetAtIndex(0).position.x, grid.GetMinPosition().y);
        }
        else if (parts.GetAtIndex(0).position.x < grid.GetMinPosition().x)
        {
            parts.GetAtIndex(0).position = new Vector3(grid.GetMaxPosition().x, parts.GetAtIndex(0).position.y);
        }
        else if (parts.GetAtIndex(0).position.x > grid.GetMaxPosition().x)
        {
            parts.GetAtIndex(0).position = new Vector3(grid.GetMinPosition().x, parts.GetAtIndex(0).position.y);
        }
    }

    private void Movement() // Checks input
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            position.y = 1;
            position.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            position.y = -1;
            position.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            position.x = -1;
            position.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            position.x = 1;
            position.y = 0;
        }
    }

    public void AddPart()// Adds a part at the tails of the snake
    {
        GameObject newPart = Instantiate(bodyPrefab, transform);
        Debug.Log(parts.GetAtIndex(0).position);
        newPart.transform.position = lastPos;
        parts.AddLast(newPart.transform);
    }

    public void DestroyParts() // Stops part of the snake from moving
    {
        int explosionPart = Random.Range(0, parts.Count - 1);
        if (parts.Count == 1)
            Death();
        else
        {
            parts.Remove(explosionPart - 1).GetComponent<SnakeMovement>().enabled = false;
        }
    }

    public void Death() // Disables movement and calls the deatscreen
    {
        canMove = false;
        SnakeGameManager.Instance.DeathScreen();
    }
    
}
