using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] currentScoreText;
    [SerializeField] private TextMeshProUGUI[] highScoreText;
    [SerializeField] private TextMeshProUGUI[] trackText;

    [SerializeField] public GameDataSO gameDataSO;

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void Start()
    {
        HighScoreDisplay();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        GameManager.GameStarted += OnGameStarted;
        GameManager.TrackCompleted += OnTrackCompleted;
        ScoreManager.ScoreUpdated += OnScoreUpdated;
        ScoreManager.HighScoreUpdated += OnHighScoreUpdated;
    }

    private void UnsubscribeFromEvents()
    {
        GameManager.GameStarted -= OnGameStarted;
        GameManager.TrackCompleted -= OnTrackCompleted;
        ScoreManager.ScoreUpdated -= OnScoreUpdated;
        ScoreManager.HighScoreUpdated -= OnHighScoreUpdated;
    }

    private void OnGameStarted()
    {
        ResetCurrentScores();
    }

    private void OnScoreUpdated(int score)
    {
        UpdateCurrentScoreDisplay(score);
    }

    private void OnHighScoreUpdated(int level, int newHighScore)
    {
        UpdateHighScoreDisplay(level, newHighScore);
    }

    private void OnTrackCompleted()
    {
        StrikeTrackText(gameDataSO.currentHoleNumber);
    }

    public void HighScoreDisplay()
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = (gameDataSO.highScores[i] == int.MaxValue) ? "" : gameDataSO.highScores[i].ToString();
        }
    }

    public void UpdateCurrentScoreDisplay(int score)
    {
        if (gameDataSO.currentHoleNumber >= 0 && gameDataSO.currentHoleNumber < currentScoreText.Length)
        {
            currentScoreText[gameDataSO.currentHoleNumber].text = score.ToString();
        }
        else
        {
            Debug.LogError($"Invalid hole number: {gameDataSO.currentHoleNumber}");
        }
    }

    public void UpdateHighScoreDisplay(int level, int newHighScore)
    {
        highScoreText[level].text = (newHighScore == int.MaxValue) ? "" : newHighScore.ToString();
    }

    public void StrikeTrackText(int currentTrack)
    {
        trackText[currentTrack].text = $"<s>Track {currentTrack + 1}</s>";
    }

    public void ResetCurrentScores()
    {
        foreach (var scoreText in currentScoreText)
        {
            scoreText.text = "";
        }
    }
}
