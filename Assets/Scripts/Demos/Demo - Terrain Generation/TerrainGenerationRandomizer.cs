using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerationRandomizer : MonoBehaviour
{
    [SerializeField] MapGenerator generator;
    [SerializeField] float delay = 1.0f;

    [SerializeField] float minNoiseScale = 20f;
    [SerializeField] float maxNoiseScale = 40f;
    [SerializeField] float noiseStepSize = 0.5f;

    [SerializeField] int minOctaves = 1;
    [SerializeField] int maxOctaves = 20;
    [SerializeField] int octaveStepSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        generator.seed = 0;
        generator.noiseScale = minNoiseScale;
        if (delay == 0f) delay = 1.0f;
        StartCoroutine(ChangeGenerationWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ChangeGenerationWithDelay()
    {
        while (true)
        {
            generator.seed++;
            generator.octaves += octaveStepSize;
            generator.noiseScale += noiseStepSize;
            if (generator.noiseScale >= maxNoiseScale || generator.noiseScale <= minNoiseScale) {
                noiseStepSize *= -1;
            }
            if (generator.octaves >= maxOctaves || generator.octaves <= minOctaves) {
                octaveStepSize *= -1;
            }
            generator.GenerateMap();
            yield return new WaitForSeconds(delay);
        }
    }
}
