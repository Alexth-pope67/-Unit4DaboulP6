using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500.0f;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerupDuration = 5;

    private float normalStrength = 10.0f; // normal knockback
    private float powerupStrength = 25.0f; // powerup knockback

    public ParticleSystem boostParticle; // Assign in Inspector

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Standard Movement
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed * Time.deltaTime);

        // Set powerup indicator position
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        // BONUS: Speed Boost and Particles
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(focalPoint.transform.forward * 10.0f, ForceMode.Impulse);
            if (boostParticle != null) boostParticle.Play();
        }
    }

    // Trigger Powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            // FIX: Start the coroutine properly
            StartCoroutine(PowerupCooldownCoroutine());
        }
    }

    // FIX: Coroutine for Powerup Duration
    IEnumerator PowerupCooldownCoroutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // FIX: Knockback Direction (Enemy - Player)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // Subtracting Player Position FROM Enemy Position gives the vector AWAY from player
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            if (hasPowerup)
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}
