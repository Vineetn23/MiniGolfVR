using UnityEngine;
using TMPro;

public class UIManager : SingletonBehaviour<UIManager>
{
    public TextMeshProUGUI[] currentScoreText;
    public TextMeshProUGUI[] highScoreText;
    public TextMeshProUGUI[] trackText;

    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.OnScoreUpdate += UpdateCurrentScoreDisplay;
        GameManager.Instance.OnHighScoreUpdate += UpdateHighScoreDisplay;
        GameManager.Instance.OnTrackStrike += StrikeTrackText;
    }

    protected override void OnDestroy()
    {
        if (GameManager.Instance != null) 
        {
            GameManager.Instance.OnScoreUpdate -= UpdateCurrentScoreDisplay;
            GameManager.Instance.OnHighScoreUpdate -= UpdateHighScoreDisplay;
            GameManager.Instance.OnTrackStrike -= StrikeTrackText;
        }
        base.OnDestroy();
    }

    private void Start()
    {
        HighScoreDisplay();
    }

    public void HighScoreDisplay()
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            int highScore = GameManager.Instance.GetHighScoreForLevel(i);

            if (highScore == int.MaxValue)
            {
                highScoreText[i].text = "0";
            }
            else
            {
                highScoreText[i].text = highScore.ToString();
            }
        }
    }

    private void UpdateCurrentScoreDisplay(int score)
    {
        currentScoreText[GameManager.Instance.currentHoleNumber].text = score.ToString();
    }

    private void UpdateHighScoreDisplay(int holeNumber, int newHighScore)
    {
        highScoreText[holeNumber].text = newHighScore.ToString();
    }

    private void StrikeTrackText(int currentTrack)
    {
        trackText[currentTrack].text = $"<s>Track {currentTrack + 1}</s>";
    }

    public void ResetCurrentScores()
    {
        // Loop through all currentScoreText elements and set their text to an empty string
        foreach (var scoreText in currentScoreText)
        {
            scoreText.text = "";
        }
    }
}
