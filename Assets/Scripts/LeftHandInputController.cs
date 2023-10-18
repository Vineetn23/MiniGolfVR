using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandInputController : MonoBehaviour
{
    public InputActionReference scorecardAction; 
    public InputActionReference gameMenuAction; 
    public GameObject scorecardCanvas; 
    public GameObject gameMenuCanvas;

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
        if (scorecardCanvas != null)
        {
            scorecardCanvas.SetActive(!scorecardCanvas.activeSelf);
        }
        gameMenuCanvas.SetActive(false);
    }

    private void ToggleGameMenu(InputAction.CallbackContext context)
    {
        if (gameMenuCanvas != null)
        {
            gameMenuCanvas.SetActive(!gameMenuCanvas.activeSelf);
        }
        scorecardCanvas.SetActive(false);
    }
}
