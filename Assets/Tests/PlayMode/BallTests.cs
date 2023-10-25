using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BallTests
{
    private Ball ball;
    private GameObject ground;
    private bool eventTriggered;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        yield return null;

        ball = Object.FindObjectOfType<Ball>();
        ground = GameObject.FindGameObjectWithTag("Ground");

        Ball.BallHitGround += OnBallHitGround;

        eventTriggered = false;
    }

    private void OnBallHitGround()
    {
        eventTriggered = true;
    }

    [UnityTest]
    public IEnumerator BallHitsGround_EventTriggered()
    {
        ball.transform.position = ground.transform.position + Vector3.up * 2.0f;

        yield return new WaitForSeconds(2.0f);

        Assert.IsTrue(eventTriggered, "BallHitGround event was not triggered after the ball collided with the ground.");
    }

    [TearDown]
    public void AfterEveryTest()
    {
        Ball.BallHitGround -= OnBallHitGround;

        eventTriggered = false;
    }
}
