using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int currentHoleNumber;

    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> startingPositions;
    [SerializeField] private Rigidbody ballRb;
    [SerializeField] private GameObject scoreboardCanvas;

    [SerializeField] private GameObject ballGameObject;

    private Vector3 offsetPlayer = new Vector3(0.250f, 0, 0);

    public int currentHitNumber = 0;
    private List<int> previousHitNumbers = new List<int>();

    private const string HighScoreKeyPrefix = "HighScore_Level_";

    public event Action<int> OnScoreUpdate;
    public event Action<int, int> OnHighScoreUpdate; 

    public event Action<int> OnTrackStrike; 

    protected override void Awake()
    {
        base.Awake(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startingPositions == null || startingPositions.Count == 0)
        {
            Debug.LogError("Starting positions are not set!");
            return;
        }

        ballRb.transform.position = startingPositions[currentHoleNumber].position;
        //player.position = startingPositions[currentHoleNumber].position + offsetPlayer;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
    }

    public void GoToNextHole()
    {

        CheckAndSaveHighScore(); 

        OnTrackStrike?.Invoke(currentHoleNumber); 

        previousHitNumbers.Add(currentHitNumber);
        currentHitNumber = 0; 

        currentHoleNumber += 1;

        if (currentHoleNumber >= startingPositions.Count)
        {
            scoreboardCanvas.SetActive(true);
            ballGameObject.SetActive(false);
        }
        else
        {
            ballRb.transform.position = startingPositions[currentHoleNumber].position;
            player.position = startingPositions[currentHoleNumber].position + offsetPlayer;
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
        }

        DisplayScore();
    }

    public void DisplayScore()
    {
        int hole = 1;
        foreach (var hitNumber in previousHitNumbers)
        {
            hole++;
        }
    }

    public void CheckAndSaveHighScore()
    {
        int currentScore = currentHitNumber;
        string highScoreKey = HighScoreKeyPrefix + currentHoleNumber;
        int highScore = PlayerPrefs.GetInt(highScoreKey, int.MaxValue); 

        if (currentScore < highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, currentScore);
            PlayerPrefs.Save();

            OnHighScoreUpdate?.Invoke(currentHoleNumber, currentScore);
        }
    }

    public int GetHighScoreForLevel(int level)
    {
        string key = HighScoreKeyPrefix + level;
        return PlayerPrefs.GetInt(key, int.MaxValue); 
    }

    public void ResetBallPosition()
    {
        Debug.Log("Reset Position");
        currentHitNumber = 0;

        ballRb.transform.position = startingPositions[currentHoleNumber].position;
        player.position = startingPositions[currentHoleNumber].position + offsetPlayer;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
    }

    public void ResetBallAndScore()
    {
        ballRb.transform.position = startingPositions[currentHoleNumber].position;
        player.position = startingPositions[currentHoleNumber].position + offsetPlayer;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        currentHitNumber = 0;

        OnScoreUpdate?.Invoke(currentHitNumber);
    }

    public void BallHit()
    {
        currentHitNumber++;
        OnScoreUpdate?.Invoke(currentHitNumber);
    }

    public void StartNewGame()
    {
        ballGameObject.SetActive(true);

        currentHoleNumber = 0;
        currentHitNumber = 0;

        ResetBallAndScore();

        UIManager.Instance.ResetCurrentScores();
    }

}
