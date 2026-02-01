using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EventTrigger : MonoBehaviour
{
    public Transform character;
    public Animator animator;
    public AudioSource voiceAudioSource;
    private static float distanceThreshold = 6f;
    public static Action<EventTrigger> onEventTriggered;
    private bool hasTriggered;
    public static Action<EndState> onEndEvent;
    private void OnEnable() {
        Movement.onMoveCompleted += _ => CheckProximity();
    }
    private void OnDisable() {
        Movement.onMoveCompleted -= _ => CheckProximity();
    }
    private void CheckProximity()
    {                       
        if(hasTriggered) return;         
        if(Vector3.Distance(Movement.Position, transform.position) > distanceThreshold) return;
        if(Physics.Raycast(transform.position, Movement.Position - transform.position, Vector3.Distance(Movement.Position, transform.position))) return;

        StartCoroutine(TriggerEvent());
    }

    public virtual IEnumerator TriggerEvent()
    {
        yield return new WaitForEndOfFrame();
        
        if(animator)animator.enabled = true;
        voiceAudioSource.Play();
        hasTriggered = true;
        onEventTriggered?.Invoke(this);
        Movement.InputActions.Player.Disable();
        Vector3 characterPos = GetCharacterMovePos();
        Quaternion  rot = Quaternion.LookRotation(Movement.Position - transform.position);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rot.y, transform.eulerAngles.z);        
        transform.DOMove(characterPos, 0.5f);
    }

    public void EndEvent(bool success)
    {
        onEndEvent.Invoke(success ? EndState.success : EndState.failure);
        transform.DOMove(Movement.Position + Movement.currentRightDir * -5, 1.5f).OnComplete(() => {character.gameObject.SetActive(false);});
    }

    public Vector3 GetCharacterMovePos()
    {
        return new Vector3(Movement.Position.x, transform.position.y, Movement.Position.z) + Movement.currentForwardsDir * 2.5f;
    }
}

public enum EndState
{
    success,
    failure
}
