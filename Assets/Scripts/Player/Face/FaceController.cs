using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActiveModifiers
{
    EyebrowOuter,
    EyebrowInner,
    EyesAndLids,
    Mouth,
}

public enum Emotes
{
    Nod,
    Shrug,
    Headshake,
    OpenMouth
}

public class FaceController : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private FaceMoveHelper faceMove;
    [SerializeField] private List<string> inputPaths = new();

    public bool isActive;
    public List<InputAction> inputActions = new();
    public List<ActiveModifiers> activeModifiers = new();
    public List<InputAction> emotesActions = new();
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerInput != null)
        {
            foreach (string curPath in inputPaths)
            {
                InputAction newInputAction = FindInputAction(playerInput, curPath);
                if (newInputAction == null)
                {
                    Debug.LogError($"Did not find an input action: {curPath}");
                }
                else
                {
                    Debug.Log($"Found input at {curPath}");
                }
                newInputAction.performed += OnActionPerformed;
                newInputAction.canceled += OnActionCanceled;

                inputActions.Add(newInputAction);
            } 
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        if (!isActive) return;

        switch(context.action.type)
        {
            case InputActionType.Value:
            case InputActionType.PassThrough:
                faceMove.MoveCurrentMuscle(context.action, activeModifiers);
            break;
            case InputActionType.Button:
                if (context.action.name == "Eyebrow outer")
                {
                    activeModifiers.Add(ActiveModifiers.EyebrowOuter);
                }
                else if (context.action.name == "Eyebrow Inner")
                {
                    activeModifiers.Add(ActiveModifiers.EyebrowInner);
                }
                else if (context.action.name == "Eyelids")
                {
                    activeModifiers.Add(ActiveModifiers.EyesAndLids);
                }
                else if (context.action.name == "Eyes")
                {
                    activeModifiers.Add(ActiveModifiers.Mouth);
                }
                else if (context.action.name == "Open Mouth")
                {
                    faceMove.Emote(context.action, Emotes.OpenMouth);
                }
                else if (context.action.name == "Shrug")
                {
                    faceMove.Emote(context.action, Emotes.Shrug);
                }
                else if (context.action.name == "Nod")
                {
                    faceMove.Emote(context.action, Emotes.Nod);
                }
                else if (context.action.name == "Head shake")
                {
                    faceMove.Emote(context.action, Emotes.Headshake);
                }
                else
                {
                    Debug.LogError("Unknown modifier");
                }
            break;
        }
    }

    private void OnActionCanceled(InputAction.CallbackContext context)
    {
        if (!isActive) return;

        ActiveModifiers modToRemove = ActiveModifiers.EyebrowOuter;
        if (context.action.name == "Eyebrow outer")
        {
            modToRemove = ActiveModifiers.EyebrowOuter;
        }
        else if (context.action.name == "Eyebrow Inner")
        {
            modToRemove = ActiveModifiers.EyebrowInner;
        }
        else if (context.action.name == "Eyelids")
        {
            modToRemove = ActiveModifiers.EyesAndLids;
        }
        else if (context.action.name == "Eyes")
        {
            modToRemove = ActiveModifiers.Mouth;
        }
                else if (context.action.name == "Open Mouth")
                {
                    faceMove.ResetEmote(context.action, Emotes.OpenMouth);
                }
                else if (context.action.name == "Shrug")
                {
                    faceMove.ResetEmote(context.action, Emotes.Shrug);
                }
                else if (context.action.name == "Nod")
                {
                    faceMove.ResetEmote(context.action, Emotes.Nod);
                }
                else if (context.action.name == "Head shake")
                {
                    faceMove.ResetEmote(context.action, Emotes.Headshake);
                }
        else
        {
            Debug.LogError("Unknown modifier");
        }

        for(int i = 0; i < activeModifiers.Count;)
        {
            if (activeModifiers[i] == modToRemove)
            {
                activeModifiers.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    #region Helper Methods
    private InputAction FindInputAction(PlayerInput input, string actionToFind)
    {
        InputAction tempAction = input.actions.FindAction(actionToFind);
        if (tempAction == null)
        {
            Debug.LogError($"Invalid input: {actionToFind}");
            return null;
        }
        
        return tempAction;   
    }

    public T GetInputValue<T>(InputAction inputAction)  where T : struct
    {
        return inputAction.ReadValue<T>();
    }
    #endregion
}
