using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EventViewManager : MonoBehaviour
{
    public Transform inGameView;
    public Transform faceView;
    private float moveSpeed = 0.65f;

    private void OnEnable() {
        EventTrigger.onEventTriggered += _ => StartEvent();
    }
    private void OnDisable() {
        EventTrigger.onEventTriggered -= _ => StartEvent();
    }
    private void StartEvent()
    {
        inGameView.transform.DOLocalMoveX(-480, moveSpeed + 0.25f);
        faceView.transform.DOLocalMoveX(480, moveSpeed);
    }
    private void EndEvent()
    {
        inGameView.transform.DOLocalMoveX(0, moveSpeed);
        faceView.transform.DOLocalMoveX(1440, moveSpeed);
    }
}
