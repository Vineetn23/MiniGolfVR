using UnityEngine;
using System;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject ballGameObject;
    Rigidbody rb;

    private void Awake()
    {
        rb = ballGameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        GameManager.ResetBallPositionEvent += ResetBallPosition;
        GameManager.ActivateBallEvent += ActivateBall;
        GameManager.DeactivateBallEvent += DeactivateBall;
        GolfClub.BallStruckWithForce += OnHitByClub;
    }

    private void OnDisable()
    {
        GameManager.ResetBallPositionEvent -= ResetBallPosition;
        GameManager.ActivateBallEvent -= ActivateBall;
        GameManager.DeactivateBallEvent -= DeactivateBall;
        GolfClub.BallStruckWithForce -= OnHitByClub;
    }

    private void ActivateBall()
    {
        ballGameObject.SetActive(true);
    }

    private void DeactivateBall()
    {
        ballGameObject.SetActive(false);
    }

    private void ResetBallPosition(Vector3 startPosition)
    {
        ballGameObject.transform.position = startPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

    private void OnHitByClub(Vector3 hitDirection, float hitForce)
    {
        rb.AddForce(hitDirection * hitForce, ForceMode.Impulse);
    }
}
