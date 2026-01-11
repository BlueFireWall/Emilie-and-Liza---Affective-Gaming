using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    
    public int score = 0;
    public float speed = 10f;
    private Rigidbody rb;

    public Transform orientation;
    float horizontalInput;
    Vector3 moveDirection;
    float verticalInput;
    public TMP_Text scoreText;
    public TMP_Text winText;
    public TMP_Text gameOverText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        MyInput();
        winGame();
    }

    void FixedUpdate() {
        MovePlayer();
    }

    // Gets input from player
    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // Moves player
    void MovePlayer() {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }
    // Checks if player has won
    private void winGame(){
        int maxScore = 244;
        if (score == maxScore){
            winText.gameObject.SetActive(true);
        }
    }

    
// Checks if player has collided with coin, power pellet, or ghost
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Coin"))
    {
        score++;
        Destroy(other.gameObject);
        scoreText.text = "Score: " + score;
    }

    // NEW: Power Pellet pickup
    if (other.CompareTag("PowerPellet"))
    {
        // PowerPellet script handles frightened mode automatically
        // so NOTHING happens here. It is handled inside PowerPellet.cs.
    }

    if (other.CompareTag("Ghost"))
    {
        float resetTimer = 5f;
        this.gameObject.SetActive(false);
        gameOverText.text += score;
        gameOverText.gameObject.SetActive(true);
        Invoke("ResetGame", resetTimer);
    }
}


    // Resets game
    private void ResetGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
