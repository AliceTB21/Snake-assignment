using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveAndLoad
{
    private int highscore;

    public int HighScore { get { return highscore; } set { value = highscore; } }

    public SaveAndLoad(SnakeGameManager manager)
    {
        highscore = manager.Score;
    }
}
