using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BindingTutorialLeter : MonoBehaviour
{
    private TextMeshProUGUI texto;
    [SerializeField] private InputActionReference actionIzquierda;
    [SerializeField] private InputActionReference actionDerecha;
    [SerializeField] private InputActionReference actionReiniciar;
    private void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        texto.text = "Usa las teclas <color=red>" + actionIzquierda.action.GetBindingDisplayString(0) + "</color> y <color=red>" + actionDerecha.action.GetBindingDisplayString(0) +
        "</color> para mover el mapa. Usa <color=red>" + actionReiniciar.action.GetBindingDisplayString(0) + "</color> si deseas reiniciar el nivel." +
        " Lleva el agua lo menos contaminada posible hacia la meta. ¡Vamos tú puedes!";
    }
}
