using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Reflection;

public class InputManagerTests
{
    private InputManager inputManager;
    private bool scorecardToggledEventTriggered;
    private bool gameMenuToggledEventTriggered;
    private GameObject scorecardCanvas;
    private GameObject gameMenuCanvas;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        yield return null;

        inputManager = Object.FindObjectOfType<InputManager>();

        scorecardCanvas = inputManager.GetType().GetField("scorecardCanvas", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(inputManager) as GameObject;
        gameMenuCanvas = inputManager.GetType().GetField("gameMenuCanvas", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(inputManager) as GameObject;

        InputManager.ScorecardToggled += OnScorecardToggled;
        InputManager.GameMenuToggled += OnGameMenuToggled;

        scorecardToggledEventTriggered = false;
        gameMenuToggledEventTriggered = false;
    }

    private void OnScorecardToggled(bool isActive)
    {
        scorecardToggledEventTriggered = true;
    }

    private void OnGameMenuToggled(bool isActive)
    {
        gameMenuToggledEventTriggered = true;
    }

    [UnityTest]
    public IEnumerator ToggleScorecard_TogglesScorecardVisibility()
    {
        Assert.IsFalse(scorecardCanvas.activeSelf, "Scorecard canvas is initially visible.");

        var context = new InputAction.CallbackContext();
        MethodInfo toggleScorecardMethod = inputManager.GetType().GetMethod("ToggleScorecard", BindingFlags.NonPublic | BindingFlags.Instance);
        toggleScorecardMethod.Invoke(inputManager, new object[] { context });

        yield return null;

        Assert.IsTrue(scorecardCanvas.activeSelf, "Scorecard canvas did not become visible.");
        Assert.IsTrue(scorecardToggledEventTriggered, "ScorecardToggled event was not triggered.");
    }

    [UnityTest]
    public IEnumerator ToggleGameMenu_TogglesGameMenuVisibility()
    {
        Assert.IsFalse(gameMenuCanvas.activeSelf, "Game menu canvas is initially visible.");

        var context = new InputAction.CallbackContext();
        MethodInfo toggleGameMenuMethod = inputManager.GetType().GetMethod("ToggleGameMenu", BindingFlags.NonPublic | BindingFlags.Instance);
        toggleGameMenuMethod.Invoke(inputManager, new object[] { context });

        yield return null;

        Assert.IsTrue(gameMenuCanvas.activeSelf, "Game menu canvas did not become visible.");
        Assert.IsTrue(gameMenuToggledEventTriggered, "GameMenuToggled event was not triggered.");
    }

    [TearDown]
    public void AfterEveryTest()
    {
        InputManager.ScorecardToggled -= OnScorecardToggled;
        InputManager.GameMenuToggled -= OnGameMenuToggled;

        scorecardToggledEventTriggered = false;
        gameMenuToggledEventTriggered = false;
    }
}
