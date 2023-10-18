using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddSpeedOnEnter : MonoBehaviour
{
    private Collider clubCollider;

    [SerializeField] private string targetTag;

    private Vector3 previousPosition;
    private Vector3 velocity;

    [SerializeField] private XRBaseController rightHandController;

    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        previousPosition = transform.position;

        clubCollider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        velocity = (transform.position - previousPosition) / Time.fixedDeltaTime;
        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            SendHapticSignal();

            Vector3 collisionPosition = clubCollider.ClosestPoint(other.transform.position);
            Vector3 collisionNormal = other.transform.position - collisionPosition;

            Vector3 projectedVelocity = Vector3.Project(velocity, collisionNormal);
            Rigidbody rb = other.attachedRigidbody;

            rb.velocity = projectedVelocity;
            audioSource.Play();

            GameManager.Instance.BallHit(); 
        }
    }


    private void SendHapticSignal()
    {       
        rightHandController.SendHapticImpulse(0.4f, 0.3f); 
    }
}
