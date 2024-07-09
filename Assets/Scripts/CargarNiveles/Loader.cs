using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public Slider progressBar;
    public float minimumLoadTime = 0f;
    private float startTime;
    void Start()
    {
        startTime = 0;
        StartCoroutine(LoadLevelAsync());
    }
    void Update() 
    {
        startTime += Time.deltaTime;
    }

    IEnumerator LoadLevelAsync()
    {
         // Guarda el tiempo de inicio

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneData.sceneToLoad);
        operation.allowSceneActivation = false; // Evita que la escena se active inmediatamente despu�s de cargar

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            // Si la carga est� casi completa y ha pasado el tiempo m�nimo, activa la escena
            if (operation.progress >= 0.9f && startTime >= minimumLoadTime)
            {
                operation.allowSceneActivation = true; // Permite la activaci�n de la escena
            }

            yield return null;
        }
    }
}


