using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HoleTests
{
    private GameObject holeObject;
    private Hole hole;
    private GameObject ballObject;
    private Collider ballCollider;
    private GameObject gameManagerObject;
    private GameManager gameManager;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        yield return null;

        gameManager = Object.FindObjectOfType<GameManager>();

        holeObject = new GameObject();
        hole = holeObject.AddComponent<Hole>();

        ballObject = new GameObject();
        ballObject.tag = "Ball";
        ballObject.AddComponent<SphereCollider>();
        ballCollider = ballObject.GetComponent<Collider>();

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(holeObject);
        Object.Destroy(ballObject);
        Object.Destroy(gameManagerObject);
        yield return null;
    }

}
