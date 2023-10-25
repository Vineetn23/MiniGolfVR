using System;
using UnityEngine;

// GameMenuController script provides functionality for the buttons in the game's menu.
public class GameMenuController : MonoBehaviour
{
    public static event Action RequestLevelRestart; 
    public static event Action RequestNewGame;

    // Restarts the current level by resetting the ball and score
    public void RestartLevel()
    {
        RequestLevelRestart?.Invoke();
    }

    // Starts a new game, resetting all necessary game variable
    public void NewGame()
    {
        RequestNewGame?.Invoke();
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
