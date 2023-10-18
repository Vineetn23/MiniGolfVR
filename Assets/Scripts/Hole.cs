using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    [SerializeField] private string targetTag = "Ball";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            GameManager.Instance.GoToNextHole();
        }
    }
}
