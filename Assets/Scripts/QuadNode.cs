using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QuadNode
{
    private struct MeshData
    {
        public List<Vector3> vertices;
        public List<Vector2> uvs;
        public List<int> triangles;
        public List<Color> colors;
    }

    private Planet planet;

    private QuadNode parent;
    private List<QuadNode> children;
    private List<QuadNode> childrenWhoHaventChildren = new List<QuadNode>();

    private ShapeGenerator shapeGenerator;
    private ClimateGenerator climateGenerator;

    private Mesh mesh;
    private MeshData meshData;

    private Vector3 position;
    private Vector3 left;
    private Vector3 forward;

    private float size;

    private int level;

    private bool HaveChildren => children != null && children.Count != 0;

    public QuadNode(Planet _planet, QuadNode _parent, ShapeGenerator _shapeGenerator,
                    ClimateGenerator _climateGenerator, int _level, Vector3 _position, Vector3 _left, Vector3 _forward,
                    float _size)
    {
        planet = _planet;
        parent = _parent;
        shapeGenerator = _shapeGenerator;
        climateGenerator = _climateGenerator;
        level = _level;
        position = _position;
        left = _left;
        forward = _forward;
        size = _size;

        position -= left * (size / 2);
        position -= forward * (size / 2);

        children = null;

        Initialize();
    }

    void Initialize()
    {
        meshData = GetMeshData();

        mesh = new Mesh
        {
                indexFormat = IndexFormat.UInt32,
                vertices = meshData.vertices.ToArray(),
                uv = meshData.uvs.ToArray(),
                triangles = meshData.triangles.ToArray(),
                colors = meshData.colors.ToArray()
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
    }

    MeshData GetMeshData()
    {
        var result = new MeshData
        {
                vertices = new List<Vector3>(),
                uvs = new List<Vector2>(),
                triangles = new List<int>(),
                colors = new List<Color>()
        };

        for (var z = 0; z <= planet.shapeSettings.nbVertices; z++)
        {
            for (var x = 0; x <= planet.shapeSettings.nbVertices; x++)
            {
                float px = (float) x / planet.shapeSettings.nbVertices;
                float pz = (float) z / planet.shapeSettings.nbVertices;

                Vector3 vx = left * (px * size);
                Vector3 vz = forward * (pz * size);

                Vector3 pointOnCube = position + vx + vz;
                Vector3 pointOnSphere = pointOnCube.normalized;

                var uv = new Vector2(climateGenerator.Evaluate(pointOnSphere), shapeGenerator.Evaluate(pointOnSphere));

                Vector3 vertex = pointOnSphere * ((1 + uv.y) * planet.shapeSettings.planetRadius);

                float moisture = uv.x.Remap(climateGenerator.moistureMinMax.Min, 0.0f,
                                            climateGenerator.moistureMinMax.Max, 1.0f);

                float elevation = uv.y.Remap(shapeGenerator.elevationMinMax.Min, 0.0f,
                                             shapeGenerator.elevationMinMax.Max, 1.0f);

                result.vertices.Add(vertex);
                result.uvs.Add(uv);

                Color colorBiome = climateGenerator.GetColorFromBiome(elevation, moisture, Mathf.Abs(vertex.y - planet.transform.position.y) / planet.shapeSettings.planetRadius);

                result.colors.Add(colorBiome);
            }
        }

        for (int vi = 0, y = 0; y < planet.shapeSettings.nbVertices; y++, vi++)
        {
            for (var x = 0; x < planet.shapeSettings.nbVertices; x++, vi++)
            {
                result.triangles.Add(vi);
                result.triangles.Add(vi + planet.shapeSettings.nbVertices + 1);
                result.triangles.Add(vi + 1);
                result.triangles.Add(vi + 1);
                result.triangles.Add(vi + planet.shapeSettings.nbVertices + 1);
                result.triangles.Add(vi + planet.shapeSettings.nbVertices + 2);
            }
        }

        return result;
    }

    public void SubDivide()
    {
        Vector3 subPosition = position;
        subPosition += left * (size / 2);
        subPosition += forward * (size / 2);

        Vector3 stepLeft = left * (size / 4);
        Vector3 stepForward = forward * (size / 4);

        float halfSize = size / 2;

        children = new List<QuadNode>
        {
                new QuadNode(planet, this, shapeGenerator, climateGenerator, level + 1,
                             subPosition - stepLeft + stepForward, left,
                             forward, halfSize),
                new QuadNode(planet, this, shapeGenerator, climateGenerator, level + 1,
                             subPosition + stepLeft + stepForward, left,
                             forward, halfSize),
                new QuadNode(planet, this, shapeGenerator, climateGenerator, level + 1,
                             subPosition - stepLeft - stepForward, left,
                             forward, halfSize),
                new QuadNode(planet, this, shapeGenerator, climateGenerator, level + 1,
                             subPosition + stepLeft - stepForward, left,
                             forward, halfSize)
        };

        Delete();
    }

    void Merge()
    {
        children[0].Delete();
        children[1].Delete();
        children[2].Delete();
        children[3].Delete();

        children.Clear();

        children = null;

        Delete();

        Initialize();
    }

    public void Check(Vector3 _positionCamera)
    {
        bool haveChildren = HaveChildren;

        childrenWhoHaventChildren.Clear();

        if (!haveChildren)
        {
            Vector3 closestPoint = mesh.bounds.ClosestPoint(_positionCamera);

            float distance = Vector3.Distance(_positionCamera, closestPoint);

            if (size >= 5.0f
                && distance < size)
            {
                SubDivide();
            }
            else if (distance > 3 * size)
            {
                parent?.childrenWhoHaventChildren.Add(this);
            }
        }
        else
        {
            foreach (QuadNode child in children)
            {
                child?.Check(_positionCamera);
            }
        }

        if (childrenWhoHaventChildren.Count == 4)
        {
            Merge();
        }
    }

    void Delete()
    {
        if (mesh != null)
        {
            mesh.Clear();
        }

        mesh = null;

        if (meshData.vertices != null)
        {
            meshData.vertices.Clear();
            meshData.uvs.Clear();
            meshData.triangles.Clear();
            meshData.colors.Clear();
        }

        meshData.vertices = null;
        meshData.uvs = null;
        meshData.triangles = null;
        meshData.colors = null;

        GC.Collect();
    }

    public void DrawMesh(Matrix4x4 _trs, Plane[] _planes)
    {
        if (HaveChildren)
        {
            foreach (QuadNode child in children)
            {
                child.DrawMesh(_trs, _planes);
            }
        }
        else
        {
            if (planet.seeEntirePlanet
                || GeometryUtility.TestPlanesAABB(_planes, mesh.bounds))
            {
                Graphics.DrawMesh(mesh, _trs, planet.climateSettings.planetMaterial, 0);
            }
        }
    }
}