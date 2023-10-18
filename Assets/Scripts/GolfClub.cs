using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GolfClub : MonoBehaviour
{
    [SerializeField] private float forceMultiplier;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private XRBaseController rightHandController;

    [SerializeField] private string targetTag;

    private Vector3 lastPosition;
    private Vector3 velocity;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();

            Collider clubCollider = GetComponent<Collider>();
            Vector3 closestPoint = clubCollider.ClosestPoint(other.transform.position);

            Vector3 direction = (other.transform.position - closestPoint).normalized;

            float speed = velocity.magnitude;
            ballRigidbody.AddForce(direction * speed * forceMultiplier, ForceMode.Impulse);

            hitSound.Play();

            SendHapticSignal();

            GameManager.Instance.BallHit();
        }
    }

    private void SendHapticSignal()
    {

        rightHandController.SendHapticImpulse(0.4f, 0.3f); 
    }
}
