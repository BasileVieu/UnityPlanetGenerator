using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public float scale = 1;
    
    [Range(1, 8)] public int octaves = 1;
    
    public float baseRoughness = 1;
    public float roughness = 2;
    public float persistence = .5f;
    
    public Vector3 centre;
}