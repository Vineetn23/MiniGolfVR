using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.ResetPlayerPositionEvent += ResetPlayerPosition;
    }

    private void OnDisable()
    {
        GameManager.ResetPlayerPositionEvent -= ResetPlayerPosition;
    }

    private void ResetPlayerPosition(Vector3 startPosition)
    {
        transform.position = startPosition;
    }
}
