using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Punctuation : MonoBehaviour
{
    private TextMeshProUGUI punctuation;
    [SerializeField] private int nivel;
    private void Start()
    {
        punctuation = GetComponent<TextMeshProUGUI>();
        punctuation.text = PlayerPrefs.GetInt("punctuation" + nivel,0).ToString()+"%";
    }
    
}
