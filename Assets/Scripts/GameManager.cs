using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action GameStarted;
    public static event Action TrackCompleted;
    public static event Action PlayAmbientSound; 
    public static event Action RequestHighScoreLoad;
    public static event Action RequestHighScoreSave;
    public static event Action<int> AddScoreEvent;
    public static event Action CheckAndSaveHighScoreEvent;
    public static event Action ResetScoreEvent;
    public static event Action<Vector3> ResetBallPositionEvent;
    public static event Action<Vector3> ResetPlayerPositionEvent;
    public static event Action ActivateBallEvent;
    public static event Action DeactivateBallEvent;

    [SerializeField] private List<Transform> startingPositions;

    [SerializeField] private GameObject scoreboardCanvas;

    private Vector3 offsetPlayer = new Vector3(0.25f, 0, 0);

    [SerializeField] public GameDataSO gameDataSO;

    private void OnEnable()
    {
        SubscribeToEvents(true);
    }

    private void OnDisable()
    {
        SubscribeToEvents(false);
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        PlayAmbientSound?.Invoke();
        RequestHighScoreLoad?.Invoke();
        if (startingPositions == null || startingPositions.Count == 0)
        {
            Debug.LogError("Starting positions are not set!");
            return;
        }

        StartNewGame();
    }

    public void StartNewGame()
    {
        gameDataSO.currentHoleNumber = 0;
        ActivateBallEvent?.Invoke();
        ResetBallPositionEvent?.Invoke(startingPositions[gameDataSO.currentHoleNumber].transform.position);
        ResetPlayerPositionEvent?.Invoke(startingPositions[gameDataSO.currentHoleNumber].transform.position + offsetPlayer);


        GameStarted?.Invoke();
    }

    public void BallHit()
    {
        AddScoreEvent?.Invoke(1);
    }

    public void GoToNextHole()
    {
        CheckAndSaveHighScoreEvent?.Invoke();

        TrackCompleted?.Invoke();

        gameDataSO.currentHoleNumber++;

        if (gameDataSO.currentHoleNumber >= startingPositions.Count)
        {
            EndGame();
        }
        else
        {
            ResetScoreEvent?.Invoke();
            ResetBallPositionEvent?.Invoke(startingPositions[gameDataSO.currentHoleNumber].transform.position);
            ResetPlayerPositionEvent?.Invoke(startingPositions[gameDataSO.currentHoleNumber].transform.position + offsetPlayer);
        }

        RequestHighScoreSave?.Invoke();
    }

    public void EndGame()
    {
        scoreboardCanvas.SetActive(true);
        DeactivateBallEvent?.Invoke();

        RequestHighScoreSave?.Invoke();
    }

    private void ResetBallAndScore()
    {
        ResetScoreEvent?.Invoke();
        ResetBallPositionEvent?.Invoke(startingPositions[gameDataSO.currentHoleNumber].transform.position);
        ResetPlayerPositionEvent?.Invoke(startingPositions[gameDataSO.currentHoleNumber].transform.position + offsetPlayer);
    }

    private void SubscribeToEvents(bool subscribe)
    {
        if (subscribe)
        {
            Hole.HoleReached += GoToNextHole;
            Ball.BallHitGround += ResetBallAndScore;
            GolfClub.BallStruck += BallHit;
            GameMenuController.RequestLevelRestart += ResetBallAndScore;
            GameMenuController.RequestNewGame += StartNewGame;
        }
        else
        {
            Hole.HoleReached -= GoToNextHole;
            Ball.BallHitGround -= ResetBallAndScore;
            GolfClub.BallStruck -= BallHit;
            GameMenuController.RequestLevelRestart -= ResetBallAndScore;
            GameMenuController.RequestNewGame -= StartNewGame;
        }
    }

}
