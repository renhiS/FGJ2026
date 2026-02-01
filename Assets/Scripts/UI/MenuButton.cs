using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    private Button button;
    public static Action onSelectButton;    
    public static Action onClickButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onSelectButton?.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectButton?.Invoke();
    }

    public void Play()
    {
        GameManager.LoadScene(1);
    }

    public void Menu()
    {
        GameManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Start() {
        button = GetComponent<Button>();                
    }

}
