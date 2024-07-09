using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirOpciones : MonoBehaviour
{
    [SerializeField] private GameObject gameObject1;
    // Start is called before the first frame update

    public void AbrirMenu()
    {
        gameObject1.SetActive(true);
    }
    public void CerrarMenu()
    {
        gameObject1.SetActive(false);
    }
    // Update is called once per frame
}
