using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

#region Enumerators
[Serializable] public enum RequiredEmote
{
    Nod,
    Shrug,
    Headshake
}

[Serializable] public struct AcceptableRange
{
    public string blendName;
    public Vector2 leftRanges;
    public Vector2 rightRanges;
    public bool succeededCheck;
}

[Serializable] public struct FaceEvent
{
    public string eventName;
    public List<AcceptableRange> acceptableRanges;
    public RequiredEmote requiredEmote;
    public bool isGood;
}
#endregion

public class FaceChecker : MonoBehaviour
{
    public List<FaceEvent> faceEvents;
    public int eventIndex = -1;

    public SkinnedMeshRenderer skin;
    public bool printDebug;

    void Start()
    {
        if (printDebug)
        {
            foreach(FaceEvent guy in faceEvents)
            {
                string logMesg = $"{guy.eventName}: ";

                foreach(AcceptableRange man in guy.acceptableRanges)
                {
                    logMesg += $"{man.blendName}, ";
                    logMesg += $"{man.leftRanges}, ";
                    logMesg += $"{man.rightRanges}, ";
                    logMesg += $"{man.succeededCheck}, ";
                }

                logMesg += $"{guy.requiredEmote}, ";
                logMesg += $"{guy.isGood} \n";

                Debug.Log(logMesg);
            }
        }
    }

    public bool CheckNextEvent(Emotes emoteSender)
    {
        eventIndex++;

        bool isGood = CheckEventAtIndex(eventIndex, emoteSender);
        
        Debug.Log(isGood ? "true" : "false");
        return isGood;
    } 

    public bool CheckEventAtIndex(int index, Emotes emoteSender)
    {
        if (index >= faceEvents.Count)
        {
            return false;
        }

        FaceEvent curEvent = faceEvents[index];

        if (curEvent.acceptableRanges.Count < 1)
        {
            return false;
        }

        Debug.Log($"Current event: {curEvent.eventName}");

        Mesh skinMesh = skin.sharedMesh;

        for (int i = 0; i < curEvent.acceptableRanges.Count; i++)
        {
            AcceptableRange checks = curEvent.acceptableRanges[i];
            for(int j = 0; j < skinMesh.blendShapeCount; j++)
            {
                if (skinMesh.GetBlendShapeName(j).Contains(checks.blendName, StringComparison.OrdinalIgnoreCase))
                {
                    float blend = skin.GetBlendShapeWeight(j);

                    if (skinMesh.GetBlendShapeName(j).Contains("Left", StringComparison.OrdinalIgnoreCase))
                    {
                        if (blend >= (checks.leftRanges.x * 100) && blend <= (checks.leftRanges.y * 100))
                        {
                            checks.succeededCheck = true;
                        }
                        else
                        {
                            curEvent.isGood = false;
                            return false;
                        }
                    }
                    else if (skinMesh.GetBlendShapeName(j).Contains("Right", StringComparison.OrdinalIgnoreCase))
                    {
                        if (blend >= (checks.rightRanges.x * 100) && (blend <= checks.rightRanges.y * 100))
                        {
                            checks.succeededCheck = true;
                        }
                        else
                        {
                            curEvent.isGood = false;
                            return false;
                        }
                    }
                    else if (skinMesh.GetBlendShapeName(j) == "Mouth Position")
                    {
                        if (blend >= (checks.leftRanges.x * 100) && blend <= (checks.leftRanges.y * 100) && blend >= (checks.rightRanges.x * 100) && blend <= (checks.rightRanges.y * 100))
                        {
                            checks.succeededCheck = true;
                        }
                        else
                        {
                            curEvent.isGood = false;
                            return false;
                        }
                    }
                }
            }
        }

        if ((int)emoteSender != (int)curEvent.requiredEmote)
        {
            curEvent.isGood = false;
            return false;
        }

        curEvent.isGood = true;

        return true;
    }
}
