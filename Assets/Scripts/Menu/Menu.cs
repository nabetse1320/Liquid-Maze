using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    private Stack<AbrirOpciones> menus;
    [SerializeField] private GameObject menu;
    [SerializeField] private AbrirOpciones botonesOpciones;
    [SerializeField] private AbrirOpciones[] botonesObcionesSecundarios;
    // Start is called before the first frame update

    public void OnAbrirMenu(InputAction.CallbackContext value) 
    {
        Debug.Log("YEAAAAAAAAAA");     /*   if(value.started)
        {
            
            if (menu != null)
            {
                if (!menu.activeSelf)
                {
                    Time.timeScale = 0;
                    menu.SetActive(true);
                }
                else
                {
                    foreach(var menu in botonesObcionesSecundarios)
                    {
                        if (menu.menuAbierto)
                        {
                            menu.CerrarMenu();
                        }
                        else
                        {
                            botonesOpciones.CerrarMenu();

                        }
                    }
                    if (!botonesOpciones.menuAbierto)
                    {
                        CerrarMenu();
                    }
                }
            }
        }
       */ 
    }
    public void CerrarMenu()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }
}
