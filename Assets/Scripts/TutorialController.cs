using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 startPos = new Vector3(-4.3f, 0, -3.5f);
    private Quaternion startRot = Quaternion.Euler(0, 90, 0);

    public void MoveToLoc()
    {
        player.position = startPos;
        player.rotation = startRot;
    }
}
