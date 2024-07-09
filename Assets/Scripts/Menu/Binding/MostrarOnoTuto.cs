using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarOnoTuto : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("CompletedLvl2") != 0)
        {
            this.gameObject.SetActive(false);
        }
    }

}
