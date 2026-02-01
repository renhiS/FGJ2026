using UnityEngine;

public class GameStateScreens : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    private void OnEnable() {
        SkinDegradationManager.onDie -= () => loseScreen.gameObject.SetActive(true); ;
        WinEvent.onWin -= () => winScreen.SetActive(true);
        SkinDegradationManager.onDie += () => loseScreen.gameObject.SetActive(true); ;
        WinEvent.onWin += () => winScreen.SetActive(true); ;
    }

    private void OnDisable() {
        SkinDegradationManager.onDie -= () => loseScreen.gameObject.SetActive(true); ;
        WinEvent.onWin -= () => winScreen.SetActive(true);
    }    
}
