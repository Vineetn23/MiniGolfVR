using UnityEngine;
using System;

public class AnimationManager : MonoBehaviour
{
    public enum HandType { LeftHand, RightHand }

    [SerializeField] private Animator leftHandAnimator;
    [SerializeField] private Animator rightHandAnimator;

    private void OnEnable()
    {
        InputManager.ScorecardToggled += UpdatePinchAnimationForScorecard;
        InputManager.GameMenuToggled += UpdateGripAnimationForGameMenu;
    }

    private void OnDisable()
    {
        InputManager.ScorecardToggled -= UpdatePinchAnimationForScorecard;
        InputManager.GameMenuToggled -= UpdateGripAnimationForGameMenu;
    }

    public void UpdateGripAnimation(HandType hand, bool shouldGrip)
    {
        float newGripAmount = shouldGrip ? 1.0f : 0.0f; // 1.0 for grip, 0.0 for ungrip
        GetAnimatorForHand(hand).SetFloat("Grip", newGripAmount);
    }

    public void UpdatePinchAnimation(HandType hand, bool shouldPinch)
    {
        float newPinchAmount = shouldPinch ? 1.0f : 0.0f; // 1.0 for pinched, 0.0 for unpinched
        GetAnimatorForHand(hand).SetFloat("Trigger", newPinchAmount);
    }

    private Animator GetAnimatorForHand(HandType hand)
    {
        switch (hand)
        {
            case HandType.LeftHand:
                return leftHandAnimator;
            case HandType.RightHand:
                return rightHandAnimator;
            default:
                Debug.LogError("Invalid hand type specified.");
                return null;
        }
    }

    private void UpdatePinchAnimationForScorecard(bool isScorecardActive)
    {
        UpdatePinchAnimation(HandType.LeftHand, isScorecardActive);
        UpdateGripAnimation(HandType.RightHand, isScorecardActive);
    }

    private void UpdateGripAnimationForGameMenu(bool isGameMenuActive)
    {
        UpdatePinchAnimation(HandType.LeftHand, isGameMenuActive);
        UpdateGripAnimation(HandType.RightHand, !isGameMenuActive);
    }
}
