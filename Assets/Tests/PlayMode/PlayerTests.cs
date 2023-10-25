using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerTests
{
    private Player player;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        yield return null;

        player = Object.FindObjectOfType<Player>();
    }

    [UnityTest]
    public IEnumerator ResetPlayerPosition_SetsPlayerToStartPosition()
    {
        Vector3 startPosition = new Vector3(0, 1, 0); 

        player.GetType().GetMethod("ResetPlayerPosition", BindingFlags.NonPublic | BindingFlags.Instance)
              .Invoke(player, new object[] { startPosition });

        yield return null;

        Assert.AreEqual(startPosition, player.transform.position);
    }

}
