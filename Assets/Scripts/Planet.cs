using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Camera mainCamera;

    private Transform cameraTransform;

    public ShapeSettings shapeSettings;
    public ClimateSettings climateSettings;

    private ShapeGenerator shapeGenerator;
    private ClimateGenerator climateGenerator;

    private List<QuadNode> nodes;

    [HideInInspector] public float distancePlanetPlayer;
    [HideInInspector] public float distancePlanetPlayerPow2;

    [HideInInspector] public bool shapeSettingsFoldout;
    [HideInInspector] public bool climateSettingsFoldout;

    public int seed;

    public bool seeEntirePlanet;

    void Start()
    {
        cameraTransform = mainCamera.transform;

        shapeGenerator = new ShapeGenerator(shapeSettings, seed);
        climateGenerator = new ClimateGenerator(climateSettings, seed);

        float halfRadius = shapeSettings.planetRadius / 2.0f;

        nodes = new List<QuadNode>
        {
                new QuadNode(this, null, shapeGenerator, climateGenerator, 1,
                             new Vector3(0.0f, 0.0f, -halfRadius), Vector3.left,
                             Vector3.down, shapeSettings.planetRadius), // back
                new QuadNode(this, null, shapeGenerator, climateGenerator, 1,
                             new Vector3(0.0f, 0.0f, halfRadius), Vector3.right,
                             Vector3.down, shapeSettings.planetRadius), // forward
                new QuadNode(this, null, shapeGenerator, climateGenerator, 1,
                             new Vector3(-halfRadius, 0.0f, 0.0f), Vector3.forward,
                             Vector3.down, shapeSettings.planetRadius), // left
                new QuadNode(this, null, shapeGenerator, climateGenerator, 1,
                             new Vector3(halfRadius, 0.0f, 0.0f), Vector3.back,
                             Vector3.down, shapeSettings.planetRadius), // right
                new QuadNode(this, null, shapeGenerator, climateGenerator, 1,
                             new Vector3(0.0f, halfRadius, 0.0f), Vector3.forward,
                             Vector3.left, shapeSettings.planetRadius), // up
                new QuadNode(this, null, shapeGenerator, climateGenerator, 1,
                             new Vector3(0.0f, -halfRadius, 0.0f), Vector3.back,
                             Vector3.left, shapeSettings.planetRadius) // down
        };

        foreach (QuadNode node in nodes)
        {
            node.SubDivide();
        }
    }

    void Update()
    {
        distancePlanetPlayer = Vector3.Distance(transform.position, cameraTransform.position);
        distancePlanetPlayerPow2 = distancePlanetPlayer * distancePlanetPlayer;

        foreach (QuadNode node in nodes)
        {
            node.Check(cameraTransform.position);
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        foreach (QuadNode node in nodes)
        {
            node.DrawMesh(transform.localToWorldMatrix, planes);
        }
    }
}