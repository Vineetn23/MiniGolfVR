using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static event Action<bool> ScorecardToggled;
    public static event Action<bool> GameMenuToggled;

    [SerializeField] private InputActionReference scorecardAction;
    [SerializeField] private InputActionReference gameMenuAction;
    [SerializeField] private GameObject scorecardCanvas;
    [SerializeField] private GameObject gameMenuCanvas;

    private void Start()
    {
        scorecardCanvas.SetActive(false);
        gameMenuCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        scorecardAction.action.Enable();
        gameMenuAction.action.Enable();

        scorecardAction.action.performed += ToggleScorecard;
        gameMenuAction.action.performed += ToggleGameMenu;
    }

    private void OnDisable()
    {
        scorecardAction.action.performed -= ToggleScorecard;
        gameMenuAction.action.performed -= ToggleGameMenu;

        scorecardAction.action.Disable();
        gameMenuAction.action.Disable();
    }

    private void ToggleScorecard(InputAction.CallbackContext context)
    {
        bool isActive = !scorecardCanvas.activeSelf; 
        scorecardCanvas.SetActive(isActive);
        gameMenuCanvas.SetActive(false); 

        ScorecardToggled?.Invoke(isActive);
    }

    private void ToggleGameMenu(InputAction.CallbackContext context)
    {
        bool isActive = !gameMenuCanvas.activeSelf; 
        gameMenuCanvas.SetActive(isActive);
        scorecardCanvas.SetActive(false); 

        GameMenuToggled?.Invoke(isActive);
    }
}
