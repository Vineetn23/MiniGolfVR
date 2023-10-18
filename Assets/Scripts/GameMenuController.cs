using UnityEngine;

public class GameMenuController : MonoBehaviour
{

    public void RestartLevel()
    {
        GameManager.Instance.ResetBallAndScore();
    }

    public void NewGame()
    {
        GameManager.Instance.StartNewGame(); 
    }

    public void QuitGame()
    {
            Application.Quit();
    }
}
