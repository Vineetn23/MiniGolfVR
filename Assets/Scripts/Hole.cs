using System;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public static event Action HoleReached;

    [SerializeField] private string targetTag = "Ball";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            HoleReached?.Invoke();
        }
    }
}
