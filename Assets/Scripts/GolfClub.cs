using System;
using UnityEngine;

public class GolfClub : BaseGolfClub
{
    public static event Action BallStruck;
    public static event Action<Vector3, float> BallStruckWithForce;
    public static event Action<HapticsManager.ControllerType, float, float> BallStruckHaptic;

    [SerializeField] private float forceMultiplier;
    [SerializeField] private string targetTag;

    private Vector3 lastPosition;
    private Vector3 velocity;
    Collider clubCollider;

    private void Start()
    {
        lastPosition = transform.position;
        clubCollider = GetComponent<Collider>();

    }

    private void Update()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleCollisionWithBall(other);
    }

    public override void HandleCollisionWithBall(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {            
            Vector3 closestPoint = clubCollider.ClosestPoint(other.transform.position);
            Vector3 direction = (other.transform.position - closestPoint).normalized;

            float speed = velocity.magnitude;
            float hitForce = speed * forceMultiplier;

            BallStruckWithForce?.Invoke(direction, hitForce);
            BallStruck?.Invoke();
            BallStruckHaptic?.Invoke(HapticsManager.ControllerType.RightController, 0.3f, 0.4f);
        }
    }
}
