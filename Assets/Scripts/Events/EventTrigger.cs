using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EventTrigger : MonoBehaviour
{
    public Transform character;
    private static float distanceThreshold = 6f;
    public static Action<EventTrigger> onEventTriggered;
    private void OnEnable() {
        Movement.onMoveCompleted += _ => CheckProximity();
    }
    private void OnDisable() {
        Movement.onMoveCompleted -= _ => CheckProximity();
    }
    private void CheckProximity()
    {                                
        if(Vector3.Distance(Movement.Position, transform.position) > distanceThreshold) return;
        if(Physics.Raycast(transform.position, Movement.Position - transform.position, Vector3.Distance(Movement.Position, transform.position))) return;

        StartCoroutine(TriggerEvent());
    }

    public virtual IEnumerator TriggerEvent()
    {
        yield return new WaitForEndOfFrame();
        
        Debug.Log("event triggered");
        onEventTriggered?.Invoke(this);
        Movement.InputActions.Player.Disable();
        Vector3 characterPos = GetCharacterMovePos();
        character.DOMove(characterPos, 0.5f);
    }

    public Vector3 GetCharacterMovePos()
    {
        return new Vector3(Movement.Position.x, character.position.y, Movement.Position.z) + Movement.currentForwardsDir * 2.5f;
    }
}
