using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private MeshCollider meshCollider;

    private List<Vector3> vertices = new List<Vector3>();
    
    void Awake()
    {
        var obj = new GameObject("Planet Collider");
        obj.transform.parent = transform;
        meshCollider = obj.AddComponent<MeshCollider>();
    }

    void Update()
    {
        Bounds bounds = GetMaxBounds();

        var size = (int)bounds.size.x;

        if (bounds.size.y > size)
        {
            size = (int)bounds.size.y;
        }

        if (bounds.size.z > size)
        {
            size = (int)bounds.size.z;
        }

        size *= 10;

        for (var z = 0; z < size; z++)
        {
            for (var x = 0; x < size; x++)
            {
                var vertex = new Vector3();
            }
        }
    }

    Bounds GetMaxBounds()
    {
        var b = new Bounds(gameObject.transform.position, Vector3.zero);

        foreach (Renderer r in gameObject.GetComponentsInChildren<Renderer>())
        {
            b.Encapsulate(r.bounds);
        }

        return b;
    }
}