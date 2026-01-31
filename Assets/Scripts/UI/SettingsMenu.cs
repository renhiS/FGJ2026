using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public GameObject startMenu;
    public Button buttonToSelect;
    [SerializeField] private InputSystemUIInputModule iptmod;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (iptmod.cancel.action.WasPerformedThisFrame())
        {
            startMenu.SetActive(true);
            gameObject.SetActive(false);
            buttonToSelect.Select();
        }

    } 
}
