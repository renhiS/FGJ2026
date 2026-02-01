using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    private static PlayerInputActions inputActions;
    public static PlayerInputActions InputActions => inputActions;
    [SerializeField] private float stepDistance = 2f;    
    [SerializeField] private float stepDuration;
    private float moveTimer;
    public static Action<List<MoveDirection>> onMoveCompleted;
    public static Action onMoveStarted;
    private static Vector3 currentPosition;
    public static Vector3 Position => currentPosition;
    public static Vector3 currentForwardsDir;
    private void Awake()
    {
        inputActions = new PlayerInputActions();    
    }
    private void Start() {
        OnMoveCompleted();
    }
    void Update()
    {
        if(moveTimer >= 0)
            moveTimer -= Time.deltaTime;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;    
        Event.onEndEvent += _ => inputActions.Player.Enable();    
    }
    void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 dir =  context.ReadValue<Vector2>().normalized;

        if(moveTimer >= 0) return;

        if(OnVerticalInput(Mathf.RoundToInt(dir.y))) return;
        OnHorizontalInput(Mathf.RoundToInt(dir.x));
    }    
    private bool OnVerticalInput(float dir)
    {
        if(dir == 0) return false;        
        if(!IsValidDirection(transform.forward * dir)) return false;            

        Vector3 newPos = transform.position + dir * transform.forward * stepDistance;
        OnMoveStarted();
        transform.DOMove(newPos, stepDuration).OnComplete(() => {OnMoveCompleted();});        
        return true;
    }

    private bool OnHorizontalInput(float dir)
    {
        if(dir == 0) return false;
        if(!IsValidDirection(transform.right * dir)) return false;

        
        Vector3 newRot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (90 * dir), transform.eulerAngles.z);
        OnMoveStarted();
        transform.DORotate(newRot, stepDuration).OnComplete(() => {OnMoveCompleted();});
        return true;
    }
    private void OnMoveStarted()
    {
        onMoveStarted?.Invoke();
        moveTimer = stepDuration + 0.05f;
    }
    private void OnMoveCompleted()
    {
        currentPosition = transform.position;
        currentForwardsDir = transform.forward;
        List<MoveDirection> availableDirections = new List<MoveDirection>();

        if(IsValidDirection(transform.forward)) availableDirections.Add(MoveDirection.forward);
        if(IsValidDirection(transform.forward * -1)) availableDirections.Add(MoveDirection.backward);
        if(IsValidDirection(transform.right)) availableDirections.Add(MoveDirection.right);
        if(IsValidDirection(transform.right * -1)) availableDirections.Add(MoveDirection.left);

        onMoveCompleted?.Invoke(availableDirections);
    }

    private bool IsValidDirection(Vector3 dir)
    {
        return !Physics.Raycast(transform.position, dir, stepDistance + 0.5f);
    }

}

public enum MoveDirection
{
    forward,
    backward,
    left,
    right
}
