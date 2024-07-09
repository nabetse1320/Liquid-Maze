using Obi;
using UnityEngine;
using UnityEngine.UIElements;

public class DisableObiParticlesOnCollision : MonoBehaviour
{
    [SerializeField] private ObiEmitter obiEmitter;
    private ObiSolver obiSolver;
    [SerializeField] private ObiCollider entrada;
    [SerializeField] private Transform salida;

    void Start()
    {
        obiSolver = obiEmitter.solver;
        obiSolver.OnCollision += Solver_OnCollision;
    }
    void OnDisable()
    {
        obiSolver.OnCollision -= Solver_OnCollision;
    }

    private void Solver_OnCollision(object sender, ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        foreach (Oni.Contact contact in e.contacts)
        {
            var cols = world.colliderHandles[contact.bodyB].owner;
            if (entrada==cols)
            {
                Vector3 localPosition = obiSolver.transform.InverseTransformPoint(salida.position);
                obiSolver.positions[contact.bodyA] = localPosition;
            }
        }
    }
}
