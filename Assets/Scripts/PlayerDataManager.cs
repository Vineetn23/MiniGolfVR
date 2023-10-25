using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField]private GameDataSO gameDataSO;

    private void OnEnable()
    {
        GameManager.RequestHighScoreLoad += LoadHighScores;
        GameManager.RequestHighScoreSave += SaveHighScores;
        ScoreManager.RequestSaveHighScores += SaveHighScores;
    }

    private void OnDisable()
    {
        GameManager.RequestHighScoreLoad -= LoadHighScores;
        GameManager.RequestHighScoreSave -= SaveHighScores;
        ScoreManager.RequestSaveHighScores -= SaveHighScores;
    }

    public void SaveHighScores()
    {
        for (int i = 0; i < gameDataSO.highScores.Length; i++)
        {
            string highScoreKey = HighScoreKeyPrefix(i);
            if (gameDataSO.highScores[i] != int.MaxValue)
            {
                PlayerPrefs.SetInt(highScoreKey, gameDataSO.highScores[i]);
            }
        }
        PlayerPrefs.Save();
    }

    public void LoadHighScores()
    {
        for (int i = 0; i < gameDataSO.highScores.Length; i++)
        {
            string highScoreKey = HighScoreKeyPrefix(i);
            if (PlayerPrefs.HasKey(highScoreKey))
            {
                gameDataSO.highScores[i] = PlayerPrefs.GetInt(highScoreKey, int.MaxValue);
            }
            else
            {
                gameDataSO.highScores[i] = int.MaxValue;
            }
        }
    }

    private string HighScoreKeyPrefix(int level)
    {
        return $"HighScore_Level_{level}";
    }
}
