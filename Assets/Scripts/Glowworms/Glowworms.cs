using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowworms : MonoBehaviour
{
    Transform trnsfrm;
    public GameObject Glowworm;
    [Range(5, 100)]
    public int count = 5;
    SphereCollider sphere;
    public float radius;
    float p, alpha, beta;

    void Awake()
    {
        trnsfrm = transform;
        sphere = GetComponent<SphereCollider>();
        radius = sphere.radius;
        Glowworm = Resources.Load<GameObject>("Prefabs/Glowworms/Glowworm");
        SpawnGlowworms(count);
    }

    void SpawnGlowworms(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject ngw = Instantiate(Glowworm, trnsfrm) as GameObject;
            Vector3 V3 = ngw.GetComponent<Glowworm>().RandomPoint(radius);
            ngw.transform.localPosition = V3;
            ngw.GetComponent<Glowworm>().CreateWay();
        }
    }
}
