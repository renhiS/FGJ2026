using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] stepSounds;

    private void OnEnable() {
        Movement.onMoveStarted -= () => PlaySound(stepSounds);
        Movement.onMoveStarted += () => PlaySound(stepSounds);
    }

    private void OnDisable() {
        Movement.onMoveStarted -= () => PlaySound(stepSounds);
    }

    private void PlaySound(AudioClip[] soundArray)
    {
        if(soundArray == null) return;
        audioSource.PlayOneShot(soundArray[Random.Range(0,soundArray.Length)]);
    }
}
