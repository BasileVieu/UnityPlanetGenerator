using UnityEngine;

[CreateAssetMenu(menuName = "Planet/ShapeSettings")]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 1;

    public int nbVertices;

    public ShapeNoiseLayer[] shapeNoiseLayers;

    [System.Serializable]
    public class ShapeNoiseLayer
    {
        public bool enabled = true;

        public NoiseSettings noiseSettings;
    }
}