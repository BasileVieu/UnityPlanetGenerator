using UnityEngine;

public class NoiseFilter
{
    private NoiseSettings settings;

    private Noise noise;

    public NoiseFilter(NoiseSettings _settings, int _seed)
    {
        settings = _settings;

        noise = new Noise(_seed);
    }

    public float Evaluate(Vector3 _point)
    {
        float noiseValue = 0;
        float roughness = settings.baseRoughness;
        float persistence = 1;

        for (var i = 0; i < settings.octaves; i++)
        {
            float v = noise.Evaluate(_point * roughness + settings.centre);
            noiseValue += v * persistence;
            roughness *= settings.roughness;
            persistence *= settings.persistence;
        }
        
        return noiseValue * settings.scale;
    }
}