using UnityEngine;
using UnityEngine.UI;

public class ParticleEffectManager : MonoBehaviour
{
    public Button createParticleEffectButton; // Assign the UI Button in the inspector
    public GameObject particlePrefab; // Assign a default particle system prefab

    void Start()
    {
        createParticleEffectButton.onClick.AddListener(CreateRandomParticleEffect);
    }

    void CreateRandomParticleEffect()
    {
        // Instantiate a particle system from the prefab
        GameObject particleObject = Instantiate(particlePrefab);
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

        // Get particle system main module to edit core properties
        var main = particleSystem.main;

        // Randomize duration
        main.duration = Random.Range(1f, 10f);

        // Randomize start size
        main.startSize = Random.Range(0.1f, 3f);

        // Randomize start speed
        main.startSpeed = Random.Range(1f, 10f);

        // Randomize start color
        main.startColor = new Color(Random.value, Random.value, Random.value, 1f);

        // Randomize gravity
        main.gravityModifier = Random.Range(-1f, 1f);

        // Randomize emission rate
        var emission = particleSystem.emission;
        emission.rateOverTime = Random.Range(10, 100);

        // Randomize shape (between a few types)
        var shape = particleSystem.shape;
        int randomShape = Random.Range(0, 3);
        switch (randomShape)
        {
            case 0: shape.shapeType = ParticleSystemShapeType.Cone; break;
            case 1: shape.shapeType = ParticleSystemShapeType.Sphere; break;
            case 2: shape.shapeType = ParticleSystemShapeType.Hemisphere; break;
        }

        // Randomize size over lifetime
        var sizeOverLifetime = particleSystem.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1, Random.Range(0.1f, 2f));

        // Randomize noise
        var noise = particleSystem.noise;
        noise.enabled = Random.Range(0, 2) == 1;
        noise.strength = Random.Range(0f, 2f);

        // Randomize rotation over lifetime
        var rotationOverLifetime = particleSystem.rotationOverLifetime;
        rotationOverLifetime.enabled = true;
        rotationOverLifetime.z = new ParticleSystem.MinMaxCurve(0, Random.Range(-180f, 180f));

        // Randomize particle texture from a pool of textures
        var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material = GetRandomParticleMaterial();

        // Position particle effect randomly within view
        particleObject.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), Random.Range(-2f, 2f));

        // Play the particle system
        particleSystem.Play();

        // Optionally, destroy the particle effect after some time
        Destroy(particleObject, main.duration + 1f);
    }

    Material GetRandomParticleMaterial()
    {
        // You can add different materials for the particles (assign textures in Unity)
        Material[] particleMaterials = new Material[3]; // Assign these in the Unity Inspector
        return particleMaterials[Random.Range(0, particleMaterials.Length)];
    }
}
