using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void RestartGame() // Calls restart game form the game manager
    {
        SnakeGameManager.Instance.RestartGame();
    }

    public void QuitGame() // Closes the application
    {
        Application.Quit();
    }
}
