using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float timestep = 0.2f;
    public float emotionMultiplier = 1.0f; // Emotion-based speed adjustment
    public float fallSpeedMultiplier = 1f;  // 1 = normal speed, <1 slower, >1 faster
    private float time;

    // The current active group
    public GameObject actualGroup;

    // Start the game by spawning the first group
    public void StartGame()
    {
        actualGroup = GetComponent<GroupSpawner>().SpawnNext();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > timestep)
        {
            time = 0;
            if (actualGroup != null)
            {
                Move(Vector3.down);
            }
        }

        CheckForInput();
    }

    void CheckForInput()
    {
        if (actualGroup == null) return;

        // Rotation
        if (Input.GetKeyDown(KeyCode.R))
            actualGroup.GetComponent<Rotation>().RotateRight();
        else if (Input.GetKeyDown(KeyCode.L))
            actualGroup.GetComponent<Rotation>().RotateLeft();

        // Horizontal movement
        if (Input.GetKeyDown(KeyCode.A))
            Move(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D))
            Move(Vector3.right);

        // Fast drop or normal fall speed
        float baseSpeed = GetComponent<Highscore>().level > 0 ? (10 - GetComponent<Highscore>().level) * 0.05f : 0.2f;
        timestep = Input.GetKey(KeyCode.S) ? 0.05f : (baseSpeed / emotionMultiplier) * fallSpeedMultiplier;

        // Update cube positions
        GetComponent<CubeArray>().GetCubePositionFromScene();
    }

    // Adjust speed based on level
    public void SetNewSpeed()
    {
        float baseSpeed = (10 - GetComponent<Highscore>().level) * 0.05f;
        timestep = (baseSpeed / emotionMultiplier) * fallSpeedMultiplier;
    }

    // Move the current group and handle collisions
    void Move(Vector3 direction)
    {
        actualGroup.transform.position += direction;

        if (!GetComponent<CubeArray>().GetCubePositionFromScene())
        {
            // Undo invalid move
            actualGroup.transform.position -= direction;

            // Play collision sound
            GameObject.Find("CantMove").GetComponent<AudioSource>().Play();

            // If moving down, spawn a new group
            if (direction == Vector3.down)
                SpawnNew();
        }
    }

    // Spawn a new group and check for game over
    private void SpawnNew()
    {
        actualGroup.GetComponent<Rotation>().isActive = false;
        actualGroup = GetComponent<GroupSpawner>().SpawnNext();
        actualGroup.GetComponent<Rotation>().isActive = true;

        if (!GetComponent<CubeArray>().GetCubePositionFromScene())
        {
            // Game over: reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // Check for completed lines
            GetComponent<CubeArray>().CheckForFullLine();
        }
    }
}
