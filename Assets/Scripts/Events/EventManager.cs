using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private EventTrigger currentEvent;    
    public Slider timer;
    private void OnEnable() {
        EventTrigger.onEventTriggered += OnEventStartCallback;        
    }
    private void OnDisable() {
        EventTrigger.onEventTriggered -= OnEventStartCallback;
    }
    public void OnEventStartCallback(EventTrigger eventTrigger)
    {
        currentEvent = eventTrigger;
        timer.value = timer.maxValue;
    }

    void Update()
    {
        if(timer.value > 0)
            timer.value -= Time.deltaTime;

        if(timer.value <= 0)
            if(currentEvent)
                TestLoseEvent();
    }
    public void TestLoseEvent()
    {
        currentEvent.EndEvent(false);
        currentEvent = null;
    }

    public void TestWinEvent()
    {
        currentEvent.EndEvent(true);
        currentEvent = null;
    }
}
