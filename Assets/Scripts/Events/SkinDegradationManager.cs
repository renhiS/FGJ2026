using System;
using UnityEngine;

public class SkinDegradationManager : MonoBehaviour
{
    public Material skinMaterial;
    public Texture2D stageOne, stageTwo, stageThree;    
    private int currentStage;
    public static Action onDie;
    private void OnEnable() {
        Event.onEndEvent += stateParam => {if(stateParam == EndState.failure) DegradeSkin();};
    }

    private void OnDisable() {
        Event.onEndEvent -= stateParam => {if(stateParam == EndState.failure) DegradeSkin();};
    }

    private void DegradeSkin()
    {
        if(currentStage == 2)
        {
            onDie?.Invoke();
            return; //die lol
        } 
        currentStage++;
        switch(currentStage)
        {
            case 1:
                skinMaterial.SetTexture("_MainTex", stageTwo);
                break;
            case 2:
                skinMaterial.SetTexture("_MainTex", stageThree);
                break;
        }
    }
}
