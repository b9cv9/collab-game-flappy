using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        
        slider = GetComponent<Slider>();
        if (AudioManager.Instance != null)
        {
            slider.value = PlayerPrefs.GetFloat("Volume", AudioManager.Instance.GetVolume());
        }
        SetVolume();
        slider.onValueChanged.AddListener(delegate { SetVolume(); });

        
    }

    public void SetVolume()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(slider.value);
            PlayerPrefs.SetFloat("Volume", slider.value);
        }
    }
}
