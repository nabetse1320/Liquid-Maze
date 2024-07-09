using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarNivelesDesbloqueados : MonoBehaviour
{
   public static void SumarNivel()
   {
        DesbloquearNiveles.nivelesYaDesbloqueados++;
        PlayerPrefs.SetInt("NivelesDesbloqueados", DesbloquearNiveles.nivelesYaDesbloqueados);
   }
    public static void CambiarNivelesBloqueados(int num)
    {
        DesbloquearNiveles.nivelesYaDesbloqueados=num;
        PlayerPrefs.SetInt("NivelesDesbloqueados", DesbloquearNiveles.nivelesYaDesbloqueados);
    }
    public static void ResetPunctuation()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            PlayerPrefs.SetInt("punctuation" + i, 0);
        }
    }
}
