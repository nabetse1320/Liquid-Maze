using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesbloquearNiveles : MonoBehaviour
{
    public static int nivelesYaDesbloqueados;
    [SerializeField] private GameObject[] nivelesBloqueados;
    [SerializeField] private GameObject[] nivelesDesbloqueados;

    private void Start()
    {
        // Cargar el valor guardado al inicio del juego
        nivelesYaDesbloqueados = PlayerPrefs.GetInt("NivelesDesbloqueados", 0);
        DesbloquearLosNiveles();
    }

    public void DesbloquearLosNiveles()
    {
        if (nivelesYaDesbloqueados>nivelesDesbloqueados.Length)
        {
            for (int i = 0; i < nivelesDesbloqueados.Length; i++)
            {
                DesbloquearNivel(i);
            }
        }
        else
        {
            for (int i = 0; i < nivelesYaDesbloqueados; i++)
            {
                DesbloquearNivel(i);
            }
        }
        
    }
    public void DesbloquearNivel(int nivel)
    {
        nivelesBloqueados[nivel].SetActive(false);
        nivelesDesbloqueados[nivel].SetActive(true);
    }
}
