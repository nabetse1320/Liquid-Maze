using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Obi;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
//using UnityEngine.InputSystem;


public class MazeManager : MonoBehaviour
{
    [System.Serializable]
    public class ScoreChangedEvent : UnityEvent<int, int> { }

    public ObiSolver solver;
    public ObiEmitter emitter;
    public FluidColorizer[] colorizers;
    public ObiCollider finishLine;
    //private UnityEngine.InputSystem.Gyroscope gyro;

    [Header("Sound")]
    [SerializeField] private AudioSource AudioSourceWater;
    public float angularAcceleration = 5;
    
    [Range(0, 1)]
    public float angularDrag = 0.2f;

    public TextMeshProUGUI completionLabel;
    public TextMeshProUGUI purityLabel;
    public GameObject[] estrellas;
    public Text puntuaje;
    public TextMeshProUGUI finishLabel;
    [SerializeField] private GameObject finishMenu;

    HashSet<int> finishedParticles = new HashSet<int>();
    HashSet<int> coloredParticles = new HashSet<int>();

    float angularSpeed = 0;
    float angle = 0;
    private bool completed;
    private bool movingRight;
    private bool movingLeft;

    private Mapa inputs;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new Mapa();
        inputs.Enable();
        if (DesbloquearNiveles.nivelesYaDesbloqueados < SceneManager.GetActiveScene().buildIndex-1)
        {
            PlayerPrefs.SetInt("CompletedLvl" + SceneManager.GetActiveScene().buildIndex, 0);
        }
        completed = PlayerPrefs.GetInt("CompletedLvl" + SceneManager.GetActiveScene().buildIndex, 0) != 0;
        solver.OnCollision += Solver_OnCollision;
        emitter.OnEmitParticle += Emitter_OnEmitParticle;
    }
    public void OnMoveRight(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            movingRight = true;
        }
    }
    public void OnMoveLeft(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            movingLeft = true;
        }
    }
    public void OnMoveRightCanceled(InputAction.CallbackContext value)
    {
        if (value.canceled)
        {
            movingRight = false;
        }
    }
    public void OnMoveLeftCancelled(InputAction.CallbackContext value)
    {
        if (value.canceled)
        {
            movingLeft = false;
        }
    }

    public void OnReset(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            ResetLvl();
        }
    }


    private void OnDestroy()
    {
        solver.OnCollision -= Solver_OnCollision;
        emitter.OnEmitParticle -= Emitter_OnEmitParticle;
    }

    void Update()
    {
        if ((movingLeft||movingRight) && AudioSourceWater.volume < PlayerPrefs.GetFloat("volumeMusicSliderEfectos"))
        {
            AudioSourceWater.volume += Time.deltaTime;
        }
        if (emitter.activeParticleCount < emitter.particleCount && AudioSourceWater.volume< PlayerPrefs.GetFloat("volumeMusicSliderEfectos"))
        {
            AudioSourceWater.volume += Time.deltaTime;
        }
        else if (emitter.activeParticleCount == emitter.particleCount && AudioSourceWater.volume >0 &&(!movingRight&&!movingLeft))
        {
            AudioSourceWater.volume -= Time.deltaTime;
        }



        if (movingLeft)
        {
            angularSpeed += angularAcceleration * Time.deltaTime;
        }
        if (movingRight)
        {
            angularSpeed -= angularAcceleration * Time.deltaTime;
        }
        angularSpeed *= Mathf.Pow(1 - angularDrag, Time.deltaTime);
        angle += angularSpeed * Time.deltaTime;

        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }


    void Emitter_OnEmitParticle(ObiEmitter em, int particleIndex)
    {
        int k = emitter.solverIndices[particleIndex];
        solver.userData[k] = solver.colors[k];
    }
    public void ResetLvl()
    {
        transform.rotation = Quaternion.identity;
        angularSpeed = angle = 0;
        finishedParticles.Clear();
        coloredParticles.Clear();
        finishMenu.SetActive(false);
        UpdateScore(finishedParticles.Count, coloredParticles.Count);
        emitter.KillAll();
    }

    private void Solver_OnCollision(ObiSolver s, ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        foreach (Oni.Contact contact in e.contacts)
        {
            // look for actual contacts only:
            if (contact.distance < 0.01f)
            {
                var cols = world.colliderHandles[contact.bodyB].owner;
                foreach (var col in colorizers)
                {
                    if (col.collider == cols)
                    {
                        solver.userData[contact.bodyA] = colorizers[0].color;
                        if (coloredParticles.Add(contact.bodyA))
                            UpdateScore(finishedParticles.Count, coloredParticles.Count);
                    }
                }
                if (finishLine == cols)
                {
                    if (finishedParticles.Add(contact.bodyA))
                        UpdateScore(finishedParticles.Count, coloredParticles.Count);
                }
            }
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < emitter.solverIndices.Length; ++i)
        {
            int k = emitter.solverIndices[i];
            emitter.solver.colors[k] = emitter.solver.userData[k];
        }
    }

    public void UpdateScore(int finishedParticles, int coloredParticles)
    {
        int completion = Mathf.CeilToInt(finishedParticles / (solver.activeParticles.count*1.0f) * 100);
        int purity = Mathf.CeilToInt((1 - coloredParticles / (solver.activeParticles.count * 1.0f)) * 100);

        completionLabel.text = completion + "% Completado";
        purityLabel.text = purity + "% Pureza";

        if (completion > 90)
        {
            if (purity > 99)
            {
                finishLabel.text = "¡100% limpia, Lo hiciste perfecto!";
                finishLabel.color = new Color(0.2f, 0.8f, 0.2f);
                puntuaje.text = purity.ToString() + " %";
                StartCoroutine(AparecerEstrellas(2));
            }
            if (purity > 90)
            {
                finishLabel.text = "Eres el mejor!";
                finishLabel.color = new Color(0.2f, 0.8f, 0.2f);
                puntuaje.text =purity.ToString()+" %";
                StartCoroutine(AparecerEstrellas(3));
            }
            else if (purity > 75)
            {
                finishLabel.text = "Lo has hecho muy bien.";
                finishLabel.color = new Color(0.5f, 0.8f, 0.2f);
                puntuaje.text = purity.ToString() + " %";
                StartCoroutine(AparecerEstrellas(2));
            }
            else if (purity > 50)
            {
                finishLabel.text = "Lo hiciste bien, pero puedes mejorar";
                finishLabel.color = new Color(0.8f, 0.5f, 0.2f);
                puntuaje.text = purity.ToString() + " %";
                StartCoroutine(AparecerEstrellas(1));
            }
            else if (purity > 25)
            {
                finishLabel.text = "Podría ser mejor";
                finishLabel.color = new Color(0.8f, 0.2f, 0.2f);
                puntuaje.text = purity.ToString() + " %";
            }
            else
            {
                
                finishLabel.text = "Intentalo de nuevo, el agua está contaminada";
                finishLabel.color = new Color(0.2f, 0.2f, 0.2f);
                puntuaje.text = purity.ToString() + " %";
            }
            savePunctiation(purity);
            finishMenu.SetActive(true);
            
        }
    }
    IEnumerator AparecerEstrellas(int estDesblo)
    {
        for (int i = 0; i < estDesblo; i++)
        {
            yield return new WaitForSeconds(0.5f);
            estrellas[i].SetActive(true);
        }
    }
    private void savePunctiation(int purity)
    {
        if (purity > PlayerPrefs.GetInt("punctuation" + SceneManager.GetActiveScene().buildIndex))
        {
            PlayerPrefs.SetInt("punctuation" + SceneManager.GetActiveScene().buildIndex, purity);
        }
        if (!completed&&purity>=75)
        {
            completed = true;
            PlayerPrefs.SetInt("CompletedLvl" + SceneManager.GetActiveScene().buildIndex, 1);
            CambiarNivelesDesbloqueados.SumarNivel();
        }
    }
}
