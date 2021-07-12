using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed = 20;
    private float turnSpeed = 50;

    private Rigidbody playerRb;
    private GameManager gameManager;
    private AudioSource playerAudio;

    public AudioClip collisionRock;

    public Image[] hearth;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (index == 3)
        {
            gameManager.GameOver();
        }

        if (gameManager.isGameActive)
        {
            if (transform.position.y < -20)
            {
                gameManager.GameOver();
            }
            else if (transform.position.y > 16.3f)
            {
                gameManager.WinGame();
            }
            else
            {
                if (playerRb.velocity.z < 5)
                {
                    playerRb.AddRelativeForce(Vector3.forward * speed * verticalInput);
                } 

                if (playerRb.velocity.x < 5 && playerRb.velocity.x > -5)
                {
                    playerRb.AddForce(Vector3.right * speed * horizontalInput);
                    if (transform.rotation.y > 0.3)
                    {
                        transform.Rotate(Vector3.up * -turnSpeed * Time.deltaTime);
                    } 
                    else if (transform.rotation.y < -0.3)
                    {
                        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                    }
                    else
                    {
                        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(collisionRock);
            if (index < 3 && gameManager.isGameActive)
            {
                Destroy(hearth[index]);
                index++;
            }
        }
    }
}
