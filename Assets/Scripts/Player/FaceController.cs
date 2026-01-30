using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FaceController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction input1;
    private InputAction input2;
    private InputAction input3;
    private InputAction input4;
    private bool canUseInput = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerInput != null)
        {
            input1 = FindInputAction(playerInput, "Face/LeftStick");
            input2 = FindInputAction(playerInput, "Face/RightStick");
            input3 = FindInputAction(playerInput, "Face/LeftTrigger");
            input4 = FindInputAction(playerInput, "Face/RightTrigger");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canUseInput)
        {
            
        }
    }

    private InputAction FindInputAction(PlayerInput input, string actionToFind)
    {
        InputAction tempAction = input.actions.FindAction(actionToFind);
        if (tempAction == null)
        {
            Debug.LogError($"Invalid input: {actionToFind}");
            canUseInput = false;
            return null;
        }
        else
        {
            canUseInput = true;
            return tempAction;   
        }
    }
}
