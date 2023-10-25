using System;
using UnityEngine;

public class Ball : BaseBall
{
    public static event Action BallHitGround;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnHitGround();
        }
    }

    public override void OnHitGround()
    {
        BallHitGround?.Invoke();
    }
}
