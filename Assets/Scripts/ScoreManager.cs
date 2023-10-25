using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> ScoreUpdated;
    public static event Action<int, int> HighScoreUpdated;
    public static event Action RequestSaveHighScores;

    private int currentScore;

    [SerializeField] public GameDataSO gameDataSO; 

    private void OnEnable()
    {
        GameManager.AddScoreEvent += AddScore;
        GameManager.CheckAndSaveHighScoreEvent += CheckAndSaveHighScore;
        GameManager.ResetScoreEvent += ResetScore;
    }

    private void OnDisable()
    {
        GameManager.AddScoreEvent -= AddScore;
        GameManager.CheckAndSaveHighScoreEvent -= CheckAndSaveHighScore;
        GameManager.ResetScoreEvent -= ResetScore;
    }

    private void Start()
    {
        ResetScore();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        ScoreUpdated?.Invoke(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        ScoreUpdated?.Invoke(currentScore);
    }

    public void CheckAndSaveHighScore()
    {
        int level = gameDataSO.currentHoleNumber;
        int highScore = gameDataSO.highScores[level];

        if (currentScore < highScore && currentScore > 0)
        {
            gameDataSO.highScores[level] = currentScore;
            RequestSaveHighScores?.Invoke();
            HighScoreUpdated?.Invoke(level, currentScore);
        }
    }

}
