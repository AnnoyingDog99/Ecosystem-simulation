using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class NoiseSettings : Updatable
{
    public int seed;
    public float scale = 25.0f;
    public int octaves = 6;
    [Range(0, 1)]
    public float persistance = 0.5f;
    public float lacunarity = 0.5f;
    public Vector2 offset;


    protected override void OnValidate()
    {
        if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
        base.OnValidate();
    }
}
