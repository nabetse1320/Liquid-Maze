using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menus : MonoBehaviour
{

    Stack<GameObject> menus;
    [SerializeField] bool isMenuInicio;
    [SerializeField] GameObject gUIInGame;
    [SerializeField] GameObject menuPrincipal;
    [SerializeField] Animator animator;
    private void Start()
    {
        if (isMenuInicio)
        {
            if (menus == null)
            {
                menus = new Stack<GameObject>();
            }
            AbrirMenu(menuPrincipal);
        }
    }
    public void ManejarMenus(InputAction.CallbackContext value)
    {
        if (value.started&&!isMenuInicio)
        {
            if (menus == null)
            {
                menus = new Stack<GameObject>();
            }
            if (menus.Count <= 0)
            {
                if (gUIInGame != null)
                {
                    gUIInGame.SetActive(false);
                }
                this.gameObject.SetActive(true);
                Time.timeScale = 0;
                AbrirMenu(menuPrincipal);
            } else
            {
                CerrarMenuActual();
            }  
        }
    }

    public void AbrirMenu(GameObject menu)
    {
        if (menus.Count > 0)
        {
            GameObject menuActual = menus.Peek();
            menuActual.SetActive(false);
        }
        menu.SetActive(true);
        menus.Push(menu);
    }

    public void CerrarMenuActual()
    {
        if (menus.Peek() == menuPrincipal&&!isMenuInicio)
        {
            StartCoroutine(Despausar());
        } else
        {
            GameObject menuActual = menus.Pop();
            menuActual.SetActive(false);
            menus.Peek().SetActive(true);
        }
    }
    IEnumerator Despausar()
    {
        if (animator != null)
        {
            animator.SetBool("cerrar", true);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        VolverAJuego();
    }

    private void VolverAJuego()
    {
        if (gUIInGame != null)
        {
            gUIInGame.SetActive(true);
        }
        Time.timeScale = 1;
        menuPrincipal.SetActive(false);
        this.gameObject.SetActive(false);
        menus.Clear();
    }
}
