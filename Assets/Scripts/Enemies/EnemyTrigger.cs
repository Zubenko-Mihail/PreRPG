using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    EnemyConnector enemyConnector;
    void Awake()
    {
        enemyConnector = transform.GetComponentInParent<EnemyConnector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Array.IndexOf(enemyConnector.Targets, other.gameObject)!=-1)
        {
            enemyConnector.listTargets.Add(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (Array.IndexOf(enemyConnector.Targets, other.gameObject) != -1)
        {
            enemyConnector.listTargets.Remove(other.gameObject);
            enemyConnector.listVisibleTargets.Remove(other.gameObject);
        }

    }
}
