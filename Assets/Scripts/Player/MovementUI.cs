using System.Collections.Generic;
using UnityEngine;

public class MovementUI : MonoBehaviour
{
    public GameObject forwardArrow, backwardsArrow, rightArrow, leftArrow;
    private void OnEnable() {
        OnDisable();
        Movement.onMoveCompleted += UpdateDirectionsUI;
        Movement.onMoveStarted += HideDirectionsUI;
        EventTrigger.onEventTriggered += _ => HideDirectionsUI();
    }
    private void OnDisable()
    {
        Movement.onMoveCompleted -= UpdateDirectionsUI;
        Movement.onMoveStarted -= HideDirectionsUI;
        EventTrigger.onEventTriggered -= _ => HideDirectionsUI();
    }
    private void HideDirectionsUI()
    {
        forwardArrow.SetActive(false);
        backwardsArrow.SetActive(false);
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

    }
    private void UpdateDirectionsUI(List<MoveDirection> availableDirections)
    {
        forwardArrow.SetActive(availableDirections.Contains(MoveDirection.forward));
        backwardsArrow.SetActive(availableDirections.Contains(MoveDirection.backward));
        rightArrow.SetActive(availableDirections.Contains(MoveDirection.right));
        leftArrow.SetActive(availableDirections.Contains(MoveDirection.left));
    }
}
