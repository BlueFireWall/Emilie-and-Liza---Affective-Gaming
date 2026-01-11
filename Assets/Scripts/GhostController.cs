using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GhostController : MonoBehaviour
{
    public enum GhostType { Blinky, Pinky, Inky, Clyde }

    [Header("Ghost Settings")]
public GhostType ghost;
public float speed = 4f;
public float turnSpeed = 5f;

// store original speed
private float normalSpeed;

    [Header("Mode Settings")]
    public Transform pacman;
    public Transform blinky;   // needed for Inky
    public Transform scatterCorner;

    CharacterController controller;

    public enum Mode { Scatter, Chase, Frightened }
    public Mode currentMode = Mode.Scatter;

    public float NormalSpeed => normalSpeed;


    void Awake()
{
    controller = GetComponent<CharacterController>();
    normalSpeed = speed; // initialize
    StartCoroutine(ModeCycle());
}

    void Update()
    {
        // Always move forward
        controller.SimpleMove(transform.forward * speed);

        // Frightened mode: just move forward, no targeting
        if (currentMode == Mode.Frightened) return;

        // Targeting for Scatter/Chase
        Vector3 target = GetTargetPosition();
        Vector3 dir = (target - transform.position).normalized;

        if (dir != Vector3.zero)
        {
            Quaternion goalRot = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, Time.deltaTime * turnSpeed);
        }
    }

    // ---------------------------
    //      MODE LOGIC
    // ---------------------------
    IEnumerator ModeCycle()
    {
        while (true)
        {
            currentMode = Mode.Scatter;
            yield return new WaitForSeconds(7f);

            currentMode = Mode.Chase;
            yield return new WaitForSeconds(20f);
        }
    }

    // ---------------------------
    //      TARGET SELECTION
    // ---------------------------
    Vector3 GetTargetPosition()
    {
        if (currentMode == Mode.Scatter)
            return scatterCorner.position;

        // CHASE MODE
        switch (ghost)
        {
            case GhostType.Blinky:
                return pacman.position;
            case GhostType.Pinky:
                return pacman.position + pacman.forward * 4f;
            case GhostType.Inky:
                return GetInkyTarget();
            case GhostType.Clyde:
                float dist = Vector3.Distance(transform.position, pacman.position);
                return dist > 8f ? pacman.position : scatterCorner.position;
        }

        return pacman.position;
    }

    Vector3 GetInkyTarget()
    {
        if (blinky == null) return pacman.position;

        Vector3 twoAhead = pacman.position + pacman.forward * 2f;
        Vector3 vector = twoAhead - blinky.position;
        return blinky.position + vector * 2f;
    }

    Vector3 GetRandomTarget()
    {
        return transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }

    // ---------------------------
    //      FRIGHTENED MODE
    // ---------------------------
    public void SetFrightened()
    {
        StopAllCoroutines();
        currentMode = Mode.Frightened;
        speed = normalSpeed / 2f;   // move slower while frightened
        StartCoroutine(ExitFrightened());
    }

    IEnumerator ExitFrightened()
    {
        yield return new WaitForSeconds(7f);
        speed = normalSpeed;          // restore speed
        StartCoroutine(ModeCycle());
    }
}
