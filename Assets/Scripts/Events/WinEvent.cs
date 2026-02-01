using System;
using System.Collections;
using UnityEngine;

public class WinEvent : EventTrigger
{
    public static Action onWin;
    public override IEnumerator TriggerEvent()
    {
        onWin?.Invoke();
        yield return null;
    }
}
