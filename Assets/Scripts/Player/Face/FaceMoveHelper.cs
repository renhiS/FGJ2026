using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FaceMoveHelper : MonoBehaviour
{
    #region Variables
    [SerializeField] private FaceBlendShapes faceBlend;
    public FaceChecker faceChecker;
    #endregion

    public void MoveCurrentMuscle(InputAction activeAction, List<ActiveModifiers> activeModifiers)
    {
        MuscleSide side = activeAction.name == "LeftStick" ? MuscleSide.Left : MuscleSide.Right;
        Vector2 inputValue = activeAction.ReadValue<Vector2>() * 100;

        foreach (ActiveModifiers modifier in activeModifiers)
        {
            switch(modifier)
            {
                case ActiveModifiers.EyebrowOuter:
                    faceBlend.MoveBrowOuter(side, inputValue);
                    break;

                case ActiveModifiers.EyebrowInner:
                    faceBlend.MoveBrowInner(side, inputValue);
                    break;

                case ActiveModifiers.Mouth:
                    faceBlend.MoveMouth(side, inputValue);
                    break;

                case ActiveModifiers.EyesAndLids:
                    faceBlend.MoveEyesAndEyelids(side, inputValue);
                    break;
            }
        }
    }

    public void Emote(InputAction activeAction, Emotes emote)
    {
        if (emote == Emotes.OpenMouth)
        {
            faceBlend.OpenMouth(activeAction.ReadValue<float>());
        }
        if (emote == Emotes.Shrug)
        {
            faceBlend.Shrug(activeAction.ReadValue<float>());
            faceChecker.CheckNextEvent(emote);
        }
        if (emote == Emotes.Nod)
        {
            faceBlend.Nod(activeAction.ReadValue<float>());
            faceChecker.CheckNextEvent(emote);
        }
        if (emote == Emotes.Headshake)
        {
            faceBlend.Headshake(activeAction.ReadValue<float>());
            faceChecker.CheckNextEvent(emote);
        }
    }
    public void ResetEmote(InputAction activeAction, Emotes emote)
    {
        if (emote == Emotes.OpenMouth)
        {
            faceBlend.OpenMouth(activeAction.ReadValue<float>());
        }
        if (emote == Emotes.Shrug)
        {
            faceBlend.Shrug(activeAction.ReadValue<float>());
        }
        if (emote == Emotes.Nod)
        {
            faceBlend.Nod(activeAction.ReadValue<float>());
        }
        if (emote == Emotes.Headshake)
        {
            faceBlend.Headshake(activeAction.ReadValue<float>());
        }
    }
}
