using UnityEngine;
using UnityEngine.UI;

public class RandomParticleEffect : MonoBehaviour
{
    // Particle system prefab reference
    public GameObject particlePrefab;

    // Reference to the UI button
    public Button createParticleEffectButton;

    void Start()
    {
        // Ensure the button is connected and add a listener for the click event
        if (createParticleEffectButton != null)
        {
            createParticleEffectButton.onClick.AddListener(CreateRandomParticleEffect);
        }
        else
        {
            Debug.LogError("Create Particle Effect Button is not assigned in the Inspector!");
        }
    }

    // Method to create and randomize a new particle effect
    void CreateRandomParticleEffect()
    {
        // Ensure the particle prefab is assigned
        if (particlePrefab != null)
        {
            // Instantiate the particle system prefab
            GameObject particleObj = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);
            ParticleSystem ps = particleObj.GetComponent<ParticleSystem>();

            // Check if the ParticleSystem component is found
            if (ps != null)
            {
                // Stop the particle system and clear existing particles
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                // Ensure the particle system is completely stopped
                if (!ps.isPlaying)
                {
                    // Access the main module to modify particle properties
                    ParticleSystem.MainModule main = ps.main;

                    // Randomize various particle system properties
                    main.startLifetime = Random.Range(1.0f, 5.0f);       // Random lifetime
                    main.startSpeed = Random.Range(2.0f, 10.0f);        // Random speed
                    main.startSize = Random.Range(0.1f, 1.5f);          // Random size
                    main.startColor = Random.ColorHSV();                // Random color
                    main.gravityModifier = Random.Range(-1.0f, 1.0f);   // Random gravity
                    main.duration = Random.Range(2.0f, 10.0f);          // Random duration
                    main.startRotation = Random.Range(0.0f, Mathf.PI * 2); // Random rotation
                    main.startDelay = Random.Range(0.0f, 2.0f);         // Random start delay

                    // Randomize the emission rate
                    ParticleSystem.EmissionModule emission = ps.emission;
                    emission.rateOverTime = Random.Range(10, 100);       // Random emission rate

                    // Enable or disable noise with a random strength
                    ParticleSystem.NoiseModule noise = ps.noise;
                    noise.enabled = Random.value > 0.5f;
                    noise.strength = Random.Range(0.0f, 2.0f);          // Random noise strength

                    // Randomize the shape of the particle emission
                    ParticleSystem.ShapeModule shape = ps.shape;
                    shape.angle = Random.Range(0, 180);                 // Random angle
                    shape.radius = Random.Range(0.1f, 2.0f);            // Random radius

                    // Restart the particle system with the new randomized settings
                    ps.Play();

                    // Destroy the particle effect after its lifetime has passed
                    Destroy(particleObj, main.duration + 2.0f);
                }
                else
                {
                    Debug.LogError("Particle system is still playing. Unable to modify duration.");
                }
            }
            else
            {
                Debug.LogError("Particle System component not found on the instantiated prefab!");
            }
        }
        else
        {
            Debug.LogError("ParticlePrefab is not assigned in the Inspector!");
        }
    }
}
