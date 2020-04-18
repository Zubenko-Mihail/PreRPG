using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityManager
{
    public static void InitializeEntities()
    {
        Resources.LoadAll("Entities/Enemies/");
    }
}
