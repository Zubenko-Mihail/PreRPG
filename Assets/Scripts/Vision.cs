using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Vision : MonoBehaviour,IComparer<RaycastHit>
{
    Ray toEnemyRay;

    Vector3 dir;
    RaycastHit hit;
    private void OnTriggerStay(Collider col)
    {
        RaycastHit[] arr;
        Renderer[] childs;
        bool hasObstacle = false;
        if (col.gameObject.tag == "Enemy")
        {

            dir = col.gameObject.transform.position - transform.position;
            toEnemyRay = new Ray(transform.position, dir);
            Debug.DrawRay(transform.position, dir * 100, Color.red);
            arr = Physics.RaycastAll(toEnemyRay, 20);
            Array.Sort(arr, Compare);
            foreach (RaycastHit hit in arr)
            {
                if (hit.collider.gameObject.tag != "Enemy" 
                    && hit.collider.gameObject.tag != "Spell" 
                    && hit.collider.gameObject.tag != "NPC" 
                    && hit.collider.gameObject.tag != "Item") hasObstacle = true;
                if (hit.collider.gameObject.tag == "Enemy" && hasObstacle)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().enabled = false;
                    childs = hit.collider.gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer ren in childs)
                    {
                        ren.enabled = false;
                    }
                }
                if (hit.collider.gameObject.tag == "Enemy" && hit.collider.gameObject.tag != "Spell" && !hasObstacle)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().enabled = true;
                    childs = hit.collider.gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer ren in childs)
                    {
                        ren.enabled = true;
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        Renderer[] childs;
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Renderer>().enabled = false;
            childs = col.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer ren in childs)
            {
                ren.enabled = false;
            }
        }
    }
    public int Compare(RaycastHit x, RaycastHit y)
    {
        return x.distance.CompareTo(y.distance);
    }
}
