using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabsManager
{
    public static List <GameObject> PrefabsList = new List<GameObject>();
    public static void SetPrefabs()
    {
        foreach(GameObject go in Resources.LoadAll<GameObject>("Prefabs"))
        {
            PrefabsList.Add(go);
        }
    }
}