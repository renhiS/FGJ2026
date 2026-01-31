using UnityEngine;

public class UISounds : MonoBehaviour
{
    public AudioClip onSelectSound;
    public AudioClip onClickSound;

    private AudioSource audioSource;
    private void OnEnable() {
        MenuButton.onSelectButton += () => PlaySound(onSelectSound);
        MenuButton.onClickButton += () => PlaySound(onClickSound);
    }
    private void OnDisable()
    {
        MenuButton.onSelectButton -= () => PlaySound(onSelectSound);
        MenuButton.onClickButton -= () => PlaySound(onClickSound);
    }
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip sound)
    {
        if(sound == null) return;
        audioSource.PlayOneShot(sound);
    }


}
