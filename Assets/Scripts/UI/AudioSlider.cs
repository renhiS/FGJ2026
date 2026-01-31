using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioSlider : MonoBehaviour
{
    public AudioMixer masterMixer;
    public string channel;
    private Slider slider;
    private void Awake() {
        slider = GetComponent<Slider>();
        slider.value = 0.65f;
        SetSound(slider.value);
    }
    public void SetSound(float soundLevel)
    {
        masterMixer.SetFloat(channel, Mathf.Log(soundLevel) * 20);
    }
}
