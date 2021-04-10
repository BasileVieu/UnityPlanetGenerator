using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings settings;

    private NoiseFilter[] noiseFilters;

    public MinMax elevationMinMax;

    public ShapeGenerator(ShapeSettings _settings, int _seed)
    {
        settings = _settings;

        noiseFilters = new NoiseFilter[_settings.shapeNoiseLayers.Length];
        
        for (var i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(_settings.shapeNoiseLayers[i].noiseSettings, _seed);
        }
        
        elevationMinMax = new MinMax();
    }

    public float Evaluate(Vector3 _pointOnUnitSphere)
    {
        float elevation = 0;

        for (var i = 0; i < noiseFilters.Length; i++)
        {
            if (settings.shapeNoiseLayers[i].enabled)
            {
                elevation += noiseFilters[i].Evaluate(_pointOnUnitSphere);
            }
        }
        
        elevationMinMax.AddValue(elevation);
        
        return elevation;
    }
}