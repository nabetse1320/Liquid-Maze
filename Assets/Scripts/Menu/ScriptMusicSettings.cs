using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public AudioClip[] audioClips; // El array de AudioClips que quieres controlar

    void Start()
    {
        // Carga la configuraci�n del volumen
        float volume = PlayerPrefs.GetFloat("volumeMusic"+slider.name);

        // Establece el volumen inicial
        SetVolume(volume);

        // Configura el valor del slider
        slider.value = volume;

        // A�ade un oyente al evento onValueChanged del slider
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // Encuentra todos los AudioSource en la escena
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            // Si el AudioSource est� reproduciendo uno de los AudioClips deseados, cambia su volumen
            foreach (AudioClip audioClip in audioClips)
            {
                if (audioSource.clip == audioClip)
                {
                    audioSource.volume = value;
                    break;
                }
            }
        }

        // Guarda la configuraci�n del volumen
        PlayerPrefs.SetFloat("volumeMusic"+slider.name, value);
    }
}