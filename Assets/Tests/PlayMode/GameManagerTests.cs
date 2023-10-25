using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class GameManagerTests
{
    private GameManager gameManager;
    private GameDataSO gameData;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        yield return null;

        gameManager = Object.FindObjectOfType<GameManager>();
        gameData = gameManager.gameDataSO; 
    }

    [UnityTest]
    public IEnumerator GoToNextHole_UpdatesCurrentHoleNumber()
    {
        int initialHoleNumber = gameData.currentHoleNumber; 

        gameManager.GoToNextHole();

        yield return null;

        Assert.AreEqual(initialHoleNumber + 1, gameData.currentHoleNumber);
    }

    [UnityTest]
    public IEnumerator StartNewGame_ResetsGameConditions()
    {
        gameData.currentHoleNumber = 5; 

        gameManager.StartNewGame();

        yield return null;

        Assert.AreEqual(0, gameData.currentHoleNumber);
    }

}
