using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    public static InputManager Instance {  get; private set; }

    public event EventHandler OnInteractAction, OnInteractAlternateAction, OnPause;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Interact_Alternate,
        Pause,
    }

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
    }

    void Start()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Dispose();
    }

    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.Interact_Alternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void Rebind(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        switch (binding)
        {
            case Binding.Move_Up:
                PerformInteractiveRebind(playerInputActions.Player.Move, onActionRebound, 1);
                break;
            case Binding.Move_Down:
                PerformInteractiveRebind(playerInputActions.Player.Move, onActionRebound, 2);
                break;
            case Binding.Move_Left:
                PerformInteractiveRebind(playerInputActions.Player.Move, onActionRebound, 3);
                break;
            case Binding.Move_Right:
                PerformInteractiveRebind(playerInputActions.Player.Move, onActionRebound, 4);
                break;
            case Binding.Interact:
                PerformInteractiveRebind(playerInputActions.Player.Interact, onActionRebound, 0);
                break;
            case Binding.Interact_Alternate:
                PerformInteractiveRebind(playerInputActions.Player.InteractAlternate, onActionRebound, 0);
                break;
            case Binding.Pause:
                PerformInteractiveRebind(playerInputActions.Player.Pause, onActionRebound, 0);
                break;
        }
    }

    private void PerformInteractiveRebind(InputAction action, Action onActionRebound, int bindingIndex)
    {
        action.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());

            PlayerPrefs.Save();

            playerInputActions.Player.Enable();
        }).Start();
    }
}
