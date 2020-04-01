using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    RaycastHit hit;
    Camera camera;
    public GameObject prev;
    Dictionary<string, Color> colors = new Dictionary<string, Color>();
    Attack attack;
    void Awake()
    {
        colors["NPC"] = Color.green;
        colors["Enemy"] = Color.red;
        colors["Item"] = Color.blue;
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }

    void FixedUpdate()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            prev = hit.collider.gameObject;
            if (hit.collider.gameObject.tag == "Enemy" ||
                hit.collider.gameObject.tag == "NPC"
                && hit.collider.gameObject.GetComponent<Renderer>().enabled == true)
            {
                if (hit.collider.gameObject.GetComponent<Outline>() == null)
                {
                    var outline = hit.collider.gameObject.AddComponent<Outline>();
                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = colors[hit.collider.gameObject.tag];
                    outline.OutlineWidth = 5f;
                }
                
                hit.collider.gameObject.GetComponent<Outline>().enabled = true;
            }
            if(hit.collider.gameObject.tag == "Item")
            {
                if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<Outline>() == null)
                {
                    var outline = hit.collider.gameObject.transform.parent.gameObject.AddComponent<Outline>();
                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = colors[hit.collider.gameObject.tag];
                    outline.OutlineWidth = 5f;
                }
                prev = hit.collider.gameObject;
                hit.collider.gameObject.GetComponentInParent<Outline>().enabled = true;
            }
        }
    }
}
