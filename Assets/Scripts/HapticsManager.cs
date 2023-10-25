using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticsManager : MonoBehaviour
{
    public enum ControllerType { LeftController, RightController }

    [SerializeField] private XRBaseController leftController;
    [SerializeField] private XRBaseController rightController;

    private void OnEnable()
    {
        GolfClub.BallStruckHaptic += SendHapticSignal;
    }

    private void OnDisable()
    {
        GolfClub.BallStruckHaptic -= SendHapticSignal;
    }

    public void SendHapticSignal(ControllerType controllerType, float amplitude, float duration)
    {
        XRBaseController controller = GetControllerForHaptics(controllerType);

        if (controller != null)
        {
            controller.SendHapticImpulse(amplitude, duration);
        }
    }

    private XRBaseController GetControllerForHaptics(ControllerType controllerType)
    {
        switch (controllerType)
        {
            case ControllerType.LeftController:
                return leftController;
            case ControllerType.RightController:
                return rightController;
            default:
                Debug.LogError("Invalid ControllerType specified.");
                return null;
        }
    }
}
