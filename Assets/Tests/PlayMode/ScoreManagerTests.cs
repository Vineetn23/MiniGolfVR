using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class ScoreManagerTests
{
    private ScoreManager scoreManager;
    private GameDataSO gameDataSO;
    private bool scoreUpdatedEventTriggered;
    private bool highScoreUpdatedEventTriggered;
    private bool requestSaveHighScoresEventTriggered;
    private int updatedScore;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        yield return null;

        scoreManager = Object.FindObjectOfType<ScoreManager>();
        gameDataSO = scoreManager.GetComponent<ScoreManager>().gameDataSO;

        ScoreManager.ScoreUpdated += OnScoreUpdated;
        ScoreManager.HighScoreUpdated += OnHighScoreUpdated;
        ScoreManager.RequestSaveHighScores += OnRequestSaveHighScores;

        scoreUpdatedEventTriggered = false;
        highScoreUpdatedEventTriggered = false;
        requestSaveHighScoresEventTriggered = false;
        updatedScore = 0;
    }

    private void OnScoreUpdated(int score)
    {
        scoreUpdatedEventTriggered = true;
        updatedScore = score;
    }

    private void OnHighScoreUpdated(int level, int score)
    {
        highScoreUpdatedEventTriggered = true;
    }

    private void OnRequestSaveHighScores()
    {
        requestSaveHighScoresEventTriggered = true;
    }

    [UnityTest]
    public IEnumerator AddScore_UpdatesCurrentScore()
    {
        int initialScore = 0;
        int scoreToAdd = 5;

        scoreManager.AddScore(scoreToAdd);

        yield return null;

        Assert.IsTrue(scoreUpdatedEventTriggered, "ScoreUpdated event was not triggered.");
        Assert.AreEqual(initialScore + scoreToAdd, updatedScore, "The current score was not updated correctly.");
    }

    [UnityTest]
    public IEnumerator ResetScore_ResetsCurrentScore()
    {
        scoreManager.AddScore(5);

        scoreUpdatedEventTriggered = false;
        scoreManager.ResetScore();

        yield return null;

        Assert.IsTrue(scoreUpdatedEventTriggered, "ScoreUpdated event was not triggered.");
        Assert.AreEqual(0, updatedScore, "The score was not reset correctly.");
    }

    [UnityTest]
    public IEnumerator CheckAndSaveHighScore_UpdatesHighScore()
    {
        int level = 0; 
        gameDataSO.highScores[level] = 10; 
        scoreManager.AddScore(5); 

        scoreManager.CheckAndSaveHighScore();

        yield return null;

        Assert.IsTrue(highScoreUpdatedEventTriggered, "HighScoreUpdated event was not triggered.");
        Assert.IsTrue(requestSaveHighScoresEventTriggered, "RequestSaveHighScores event was not triggered.");
        Assert.AreEqual(5, gameDataSO.highScores[level], "The high score was not updated correctly.");
    }


    [UnityTearDown]
    public void AfterEveryTest()
    {
        ScoreManager.ScoreUpdated -= OnScoreUpdated;
        ScoreManager.HighScoreUpdated -= OnHighScoreUpdated;
        ScoreManager.RequestSaveHighScores -= OnRequestSaveHighScores;

        scoreUpdatedEventTriggered = false;
        highScoreUpdatedEventTriggered = false;
        requestSaveHighScoresEventTriggered = false;
        updatedScore = 0;
    }
}
