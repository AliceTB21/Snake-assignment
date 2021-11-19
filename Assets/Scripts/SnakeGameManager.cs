using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SnakeGameManager : MonoBehaviour
{
    private static SnakeGameManager snakeGame;
    List<GameObject> items = new List<GameObject>();
    [SerializeField] private Snake snakePrefab;

    [SerializeField] private GameObject deathPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private int score;
    [SerializeField] private Grid grid;
    [SerializeField] private int highScore;

    public static SnakeGameManager Instance
    {
        get { if(snakeGame == null)
            {
                snakeGame = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<SnakeGameManager>();
            }
            return snakeGame;
        }
    }

    public int Score { get { return score; } }
    public void AddItem(GameObject newItem) // Adds item to list
    {
        items.Add(newItem);
    }
    public void AddScore(int addScore) // Increases the score and updates score ui
    {
        score += addScore;
        scoreText.text = score.ToString();
    }

    public void RestartGame() // Destroys all items on grid and instantiates a new snake
    {
        deathPanel.SetActive(false);
        foreach(GameObject item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
        score = 0;
        scoreText.text = score.ToString();
        SpawnFood();

        Instantiate(snakePrefab, transform);

        SaveAndLoad info = SaveSystem.LoadScore();
        if (info != null)
            highScore = info.HighScore;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(grid == null)
        {
           grid = GetComponent<Grid>();
        }

        SaveAndLoad info = SaveSystem.LoadScore();
        if(info != null)
        highScore = info.HighScore;
    }

    public void DeathScreen() // Makes the death screen visible
    {
        deathPanel.SetActive(true);
        if(score > highScore)
        {
            SaveSystem.SaveScore(this);
            highScoreText.text = "New Highscore: " + score.ToString();
        }
        else
        {
            highScoreText.text = "Highscore: " + highScore.ToString();
        }



    }

    public void SpawnFood() // Calls spawn food from grid
    {
        grid.SpawnFood();
        Debug.Log("Spawning food");
    }
}
