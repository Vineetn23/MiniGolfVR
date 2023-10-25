using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerTests
{
    private UIManager uiManager;
    private GameDataSO gameDataSO;
    private TextMeshProUGUI[] currentScoreText;
    private TextMeshProUGUI[] highScoreText;
    private TextMeshProUGUI[] trackText;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        yield return null;

        uiManager = Object.FindObjectOfType<UIManager>();
        Assert.IsNotNull(uiManager, "UIManager not found in the scene.");

        currentScoreText = (TextMeshProUGUI[])typeof(UIManager)
            .GetField("currentScoreText", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(uiManager);
        highScoreText = (TextMeshProUGUI[])typeof(UIManager)
            .GetField("highScoreText", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(uiManager);
        trackText = (TextMeshProUGUI[])typeof(UIManager)
            .GetField("trackText", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(uiManager);

        gameDataSO = uiManager.gameDataSO;
        Assert.IsNotNull(gameDataSO, "GameDataSO not found.");
    }

    [UnityTest]
    public IEnumerator UpdateCurrentScoreDisplay_UpdatesText()
    {
        int score = 5;
        uiManager.UpdateCurrentScoreDisplay(score);

        yield return null;

        Assert.AreEqual(score.ToString(), currentScoreText[0].text);
    }

    [UnityTest]
    public IEnumerator UpdateHighScoreDisplay_UpdatesText()
    {
        int highScore = 10;
        uiManager.UpdateHighScoreDisplay(0, highScore);

        yield return null;

        Assert.AreEqual(highScore.ToString(), highScoreText[0].text);
    }

    [UnityTest]
    public IEnumerator StrikeTrackText_StrikesThroughText()
    {
        uiManager.StrikeTrackText(0);

        yield return null;

        Assert.AreEqual("<s>Track 1</s>", trackText[0].text);
    }

    [UnityTest]
    public IEnumerator ResetCurrentScores_ClearsText()
    {
        foreach (var textMesh in currentScoreText)
        {
            textMesh.text = "10";
        }

        uiManager.ResetCurrentScores();

        yield return null;

        foreach (var textMesh in currentScoreText)
        {
            Assert.AreEqual("", textMesh.text);
        }
    }

}
