using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private void OnEnable() {
        OnDisable();
        EventTrigger.onEventTriggered += _ => transform.DOLookAt(_.GetCharacterMovePos(), 0.5f);        
    }
    private void OnDisable()
    {
        EventTrigger.onEventTriggered -= _ => transform.DOLookAt(_.GetCharacterMovePos(), 0.5f);
    }
}
