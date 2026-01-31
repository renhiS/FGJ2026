using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private float stepDistance = 2f;    
    [SerializeField] private float stepDuration;
    private float moveTimer;
    private void Awake()
    {
        inputActions = new PlayerInputActions();    
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
        if(Physics.Raycast(transform.position, transform.forward * dir, stepDistance + 0.5f)) return false;            

        transform.DOMove(transform.position + dir * transform.forward * stepDistance, stepDuration);
        moveTimer = stepDuration + 0.05f;
        return true;
    }

    private bool OnHorizontalInput(float dir)
    {
        if(dir == 0) return false;
        if(Physics.Raycast(transform.position, transform.right * dir, stepDistance + 2)) return false;

        
        Vector3 newRot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (90 * dir), transform.eulerAngles.z);
        transform.DORotate(newRot, stepDuration);
        moveTimer = stepDuration + 0.05f;
        return true;
    }

}
