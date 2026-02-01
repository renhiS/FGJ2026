using System;
using UnityEngine;

public class SkinDegradationManager : MonoBehaviour
{
    public Material skinMaterial;
    public Texture2D stageOne, stageTwo, stageThree;    
    private int currentStage;
    public static Action onDie;
    private void Start() {
        skinMaterial.SetTexture("_BaseMap", stageOne); 
    }
    private void OnEnable() {
        EventTrigger.onEndEvent += stateParam => {if(stateParam == EndState.failure) DegradeSkin();};
    }

    private void OnDisable() {
        EventTrigger.onEndEvent -= stateParam => {if(stateParam == EndState.failure) DegradeSkin();};
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
                skinMaterial.SetTexture("_BaseMap", stageTwo); 
                break;
            case 2:
                skinMaterial.SetTexture("_BaseMap", stageThree); 
                break;
        }
    }
}
