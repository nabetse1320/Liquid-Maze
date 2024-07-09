using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullscreenToggle : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        // Carga la configuraci�n de pantalla completa
        bool isFullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;

        // Establece el modo de pantalla inicial
        Screen.fullScreen = isFullscreen;

        // Configura el estado del toggle
        toggle.isOn = isFullscreen;

        // A�ade un oyente al evento onValueChanged del toggle
        toggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // Cambia el modo de pantalla
        Screen.fullScreen = isFullscreen;

        // Guarda la configuraci�n de pantalla completa
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }
}
