using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class cambiarNivel : MonoBehaviour
{
    [SerializeField] private bool abrirEscenaDeCarga=true;
    [SerializeField] private AnimationClip clipTransition;
    private Animator transitionAnimator;
    [SerializeField] private GameObject panel;
    private float transitionTime;
    private bool bajarVol =false;
    private bool subirVol =false;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(true);
        transitionAnimator = panel.GetComponent<Animator>();
        transitionTime = clipTransition.length;
        subirVol = true;
        bajarVol = false;
        Invoke("ActivarCanva", transitionTime);

    }
    private void Update()
    {
        if (subirVol && AudioListener.volume < PlayerPrefs.GetFloat("volumeAll"))
        {
            AudioListener.volume += transitionTime * Time.deltaTime;
        }
        else if (AudioListener.volume == 1)
        {
            subirVol = false;
        }
        if (bajarVol && AudioListener.volume > 0)
        {
            AudioListener.volume += -transitionTime * Time.deltaTime;
        }
    }

    public void LoadNextScene(int Scene)
    {
        StartCoroutine(SceneLoad(Scene));
    }


    public IEnumerator SceneLoad(int Scene)
    {
        panel.SetActive(true);
        //Time.timeScale = 0;
        transitionAnimator.SetTrigger("StartTransition");
        bajarVol = true;
        subirVol =false;
        yield return new WaitForSecondsRealtime(transitionTime);
        if(Time.timeScale==0)
        {
            Time.timeScale = 1;
        }
        if (abrirEscenaDeCarga)
        {
            SceneData.sceneToLoad = Scene;
            SceneManager.LoadScene(0);
        }else
        {
            SceneManager.LoadScene(Scene);
        }
        

    }
    public void QuitGame()
    {
        StartCoroutine(CerrarJuego());
    }
    public IEnumerator CerrarJuego()
    {
        panel.SetActive(true);
        //Time.timeScale = 0;
        transitionAnimator.SetTrigger("StartTransition");
        bajarVol = true;
        subirVol = false;
        yield return new WaitForSecondsRealtime(transitionTime);
        Application.Quit();
    }

    private void ActivarCanva()
    {
        panel.SetActive(false);
    }
}
