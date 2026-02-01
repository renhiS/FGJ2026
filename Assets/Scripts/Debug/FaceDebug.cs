using TMPro;
using UnityEngine;

public class FaceDebug : MonoBehaviour
{
    public FaceController faceController;
    public TextMeshProUGUI textMeshPro;
    public bool activeDebug;

    // Update is called once per frame
    void Update()
    {
        if (activeDebug && textMeshPro != null)
        {
            textMeshPro.text = $"{faceController.GetInputValue<Vector2>(faceController.inputActions[0]).x} {faceController.GetInputValue<Vector2>(faceController.inputActions[0]).y} \n";
            textMeshPro.text += $"{faceController.GetInputValue<Vector2>(faceController.inputActions[1]).x} {faceController.GetInputValue<Vector2>(faceController.inputActions[1]).y} \n";

            foreach(ActiveModifiers mod in faceController.activeModifiers)
            {
                switch (mod)
                {
                    case ActiveModifiers.EyebrowOuter:
                        textMeshPro.text += "EyebrowOuter ";
                        break;
                    case ActiveModifiers.EyebrowInner:
                        textMeshPro.text += "EyebrowInner ";
                        break;
                    case ActiveModifiers.EyesAndLids:
                        textMeshPro.text += "Eyelid ";
                        break;
                    case ActiveModifiers.Mouth:
                        textMeshPro.text += "Eye ";
                        break;
                }
            }
        }
    }
}
