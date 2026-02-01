using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new event", menuName = "events/new event")]
public class Event : ScriptableObject
{
    public static Action<EndState> onEndEvent;
}

public enum EndState
{
    success,
    failure
}
