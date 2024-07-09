using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursormanager : MonoBehaviour
{
    [SerializeField] private Texture2D[] cursorTextureArray;
    [SerializeField] private int frameCount;
    [SerializeField] private float frameRate;

    [Header("Ocultar Cursor")]
    [SerializeField] private bool ocultarCursorConElTiempo;
    [SerializeField] private float timeToHideCursor = 5.0f; // Tiempo en segundos para ocultar el cursor
    private float timer;
    private Vector3 lastMousePosition;

    private int currentFrame;
    private float frameTimer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTextureArray[0], new Vector2(0, 0), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        // ------------------------------ Animación -----------------------------------------------
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorTextureArray[currentFrame], new Vector2(0, 0), CursorMode.Auto);
        }

        if (ocultarCursorConElTiempo)
        {
            //------------------------------ Ocultar Mouse --------------------------------------------
            
            if (lastMousePosition != Input.mousePosition)
            {
                // Si se ha movido, reinicia el temporizador y muestra el cursor
                timer = 0;
                Cursor.visible = true;
            }
            else
            {
                // Si el mouse no se ha movido, incrementa el temporizador
                timer += Time.deltaTime;
                // Si el temporizador supera el tiempo establecido, oculta el cursor
                if (timer > timeToHideCursor)
                {
                    Cursor.visible = false;
                }
            }
            // Actualiza la última posición del mouse
            lastMousePosition = Input.mousePosition;
        }

        
    }
}
