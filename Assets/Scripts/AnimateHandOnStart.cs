using UnityEngine;

public class AnimateHandOnStart : MonoBehaviour
{
    [SerializeField] private AnimationManager animationManager;

    private void Start()
    {
        InitializeAnimation();
    }

    // This method is public and can be called from tests
    public void InitializeAnimation()
    {
        if (animationManager != null)
        {
            animationManager.UpdateGripAnimation(AnimationManager.HandType.RightHand, true);
        }
    }
}
