using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField] private FaceController faceController;    
    private EventTrigger currentEvent;    
    public Slider timer;
    private void OnEnable() {
        
        EventTrigger.onEventTriggered += OnEventStartCallback;        
        FaceMoveHelper.onCheckEvent += result => {if(result) WinEvent(); else LoseEvent();};
    }
    private void OnDisable() {
        EventTrigger.onEventTriggered -= OnEventStartCallback;
        FaceMoveHelper.onCheckEvent -= result => {if(result) WinEvent(); else LoseEvent();};
    }
    public void OnEventStartCallback(EventTrigger eventTrigger)
    {
        faceController.isActive = true;
        currentEvent = eventTrigger;
        timer.value = timer.maxValue;
    }

    void Update()
    {
        if(timer.value > 0)
            timer.value -= Time.deltaTime;

        if(timer.value <= 0)
            if(currentEvent)
                LoseEvent();
    }
    public void LoseEvent()
    {
        StartCoroutine(EndEventCoroutine());

        currentEvent.EndEvent(false);
        currentEvent = null;
    }

    public void WinEvent()
    {
        StartCoroutine(EndEventCoroutine());
        
        currentEvent.EndEvent(true);
        currentEvent = null;
    }    

    private IEnumerator EndEventCoroutine()
    {
        yield return new WaitForSeconds(1f);
        faceController.isActive = false;
    }
}
