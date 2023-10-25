using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class GameMenuControllerTests
{
    private GameMenuController gameMenuController;
    private bool requestLevelRestartEventTriggered;
    private bool requestNewGameEventTriggered;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        yield return null;

        gameMenuController = Object.FindObjectOfType<GameMenuController>();

        GameMenuController.RequestLevelRestart += OnRequestLevelRestart;
        GameMenuController.RequestNewGame += OnRequestNewGame;

        requestLevelRestartEventTriggered = false;
        requestNewGameEventTriggered = false;
    }

    private void OnRequestLevelRestart()
    {
        requestLevelRestartEventTriggered = true;
    }

    private void OnRequestNewGame()
    {
        requestNewGameEventTriggered = true;
    }

    [UnityTest]
    public IEnumerator RestartLevel_TriggerRequestLevelRestartEvent()
    {
        gameMenuController.RestartLevel();

        yield return null;

        Assert.IsTrue(requestLevelRestartEventTriggered, "RequestLevelRestart event was not triggered.");
    }

    [UnityTest]
    public IEnumerator NewGame_TriggerRequestNewGameEvent()
    {
        gameMenuController.NewGame();

        yield return null;

        Assert.IsTrue(requestNewGameEventTriggered, "RequestNewGame event was not triggered.");
    }

    [UnityTearDown]
    public void AfterEveryTest()
    {
        GameMenuController.RequestLevelRestart -= OnRequestLevelRestart;
        GameMenuController.RequestNewGame -= OnRequestNewGame;

        requestLevelRestartEventTriggered = false;
        requestNewGameEventTriggered = false;
    }
}
