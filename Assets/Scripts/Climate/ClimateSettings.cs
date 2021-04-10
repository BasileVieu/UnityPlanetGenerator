using UnityEngine;

[CreateAssetMenu(menuName = "Planet/ClimateSettings")]
public class ClimateSettings : ScriptableObject
{
    public Material planetMaterial;

    [Range(0.0f, 1.0f)] public float oceanElevation = 0.1f;
    [Range(0.0f, 1.0f)] public float polesLatitude = 0.1f;

    public bool adjustBiomesWithOcean = true;

    [Range(-50.0f, 50.0f)] public float equatorTemperature = 40.0f;
    [Range(-50.0f, 50.0f)] public float polesTemperature = -30.0f;

    public TemperatureDistribution temperatureDistribution;
    public MoistureDistribution moistureDistribution;

    public MoistureNoiseLayer[] moistureNoiseLayers;

    [System.Serializable]
    public class MoistureNoiseLayer
    {
        public bool enabled = true;

        public NoiseSettings noiseSettings;
    }

    [System.Serializable]
    public class TemperatureDistribution
    {
        [Range(0.0f, 1.0f)] public float polar = 0.1f;
        [Range(0.0f, 1.0f)] public float subPolar = 0.1f;
        [Range(0.0f, 1.0f)] public float boreal = 0.2f;
        [Range(0.0f, 1.0f)] public float coolTemperate = 0.2f;
        [Range(0.0f, 1.0f)] public float warmTemperate = 0.2f;
        [Range(0.0f, 1.0f)] public float subTropical = 0.1f;
        [Range(0.0f, 1.0f)] public float tropical = 0.1f;
    }

    [System.Serializable]
    public class MoistureDistribution
    {
        [Range(0.0f, 1.0f)] public float superHumid = 0.1f;
        [Range(0.0f, 1.0f)] public float perHumid = 0.1f;
        [Range(0.0f, 1.0f)] public float humid = 0.1f;
        [Range(0.0f, 1.0f)] public float subHumid = 0.2f;
        [Range(0.0f, 1.0f)] public float semiArid = 0.2f;
        [Range(0.0f, 1.0f)] public float arid = 0.1f;
        [Range(0.0f, 1.0f)] public float perArid = 0.1f;
        [Range(0.0f, 1.0f)] public float superArid = 0.1f;
    }
}