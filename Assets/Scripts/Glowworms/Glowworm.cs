using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowworm : MonoBehaviour
{
    public List<Vector3> Way = new List<Vector3>();
    Transform obj;
    public float radius;
    bool is_way = false;

    int back_point;
    int next_point;
    public float step;
    //float speeed[]
    float progress;

    [Space]
    public Vector3 P0, P1, P2, P3;

    void Awake()
    {
        obj = transform;
        radius = obj.GetComponentInParent<Glowworms>().radius;
        back_point = 0;
        next_point = 1;
        step = Random.Range(0.05f, 0.25f);
        progress = 0;
    }
    
    void FixedUpdate()
    {
        Movement();
    }

    public void CreateWay(Vector3 GlowwormsPos)
    {
        P0 = obj.position;
            P1 = P0 + RandomPoint(radius);
        P3 = GlowwormsPos + RandomPoint(radius);
            P2 = P3 + RandomPoint(radius);
        for (float t = 0; t < 0.96f; t += 0.05f)
        {
            Way.Add((1 - t) * (1 - t) * (1 - t) * P0 + 3 * (1 - t) * (1 - t) * t * P1 + 3 * (1 - t) * t * t * P2 + t * t * t * P3);
        }
        P0 = P3;
            P1 = P0 + RandomPoint(radius);
        P3 = obj.position;
            P2 = P3 + RandomPoint(radius);
        for (float t = 0; t < 0.96f; t += 0.05f)
        {
            Way.Add((1 - t) * (1 - t) * (1 - t) * P0 + 3 * (1 - t) * (1 - t) * t * P1 + 3 * (1 - t) * t * t * P2 + t * t * t * P3);
        }
        is_way = true;
    }

    public Vector3 RandomPoint(float rad)
    {
        float p = Random.Range(0, rad);
        float alpha = Random.Range(0, 2 * Mathf.PI);
        float beta = Random.Range(0, Mathf.PI);
        Vector3 V3 = new Vector3(p * Mathf.Cos(alpha) * Mathf.Sin(beta), p * Mathf.Sin(alpha) * Mathf.Sin(beta), p * Mathf.Cos(beta));
        return V3;
    }

    void Movement()
    {
        if (is_way)
        {
            obj.position = Vector3.Lerp(Way[back_point], Way[next_point], progress);
            progress += step;
            if (progress > 1.01f)
            {
                progress = 0;
                back_point = (back_point + 1) % 40;
                next_point = (next_point + 1) % 40;
            }
        }
    }
}
