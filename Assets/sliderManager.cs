using UnityEngine;
using UnityEngine.UI;


public class sliderManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    public void UpdateMusic()
    {
        AudioManager.Instance.SetMusicVolume(musicSlider.value);
    }
    public void UpdateSFX()
    {
        AudioManager.Instance.SetsfxVolume(sfxSlider.value);
    }
}
