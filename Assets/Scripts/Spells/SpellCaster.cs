using System.Collections;
using System.Collections.Generic;
using Spells;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    RaycastHit hit;
    const float qCD = 3f, wCD = 5f, eCD = 6f;
    float qpassed = 10, wpassed = 10, epassed = 10;
    private bool qpressed, wpressed, epressed, hasTarget;
    Camera camComponent;
    LineRenderer LR;
    GameObject targGO;
    void Awake()
    {
        camComponent = GameObject.Find("Main Camera").GetComponent<Camera>();
        LR = GameObject.Find("Main Camera").GetComponent<LineRenderer>();
    }

    void Update()
    {
        CastSpells(); 
    }
    void CastSpells()
    {
        if (Input.GetKey(KeyCode.S))
        {
            qpressed = false;
            wpressed = false;
            epressed = false;
            LR.enabled = false;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
        }
        #region q
        if (Input.GetKeyDown(KeyCode.Q) && qpassed >= qCD)
        {
            wpressed = false;
            epressed = false;
            qpressed = !qpressed;
            LR.enabled = !LR.enabled;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
        }
        if (qpressed)
        {
            Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);
            Vector3 targ = transform.position;
            if (Physics.Raycast(ray, out hit, 100, ~(1 << LayerMask.NameToLayer("SpellRaycastIgnore")), QueryTriggerInteraction.Ignore))
            {
                targ = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
            LR.enabled = true;
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, targ);
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorTarg"), new Vector2(50, 50), CursorMode.Auto);
        }
        if (qpressed && Input.GetKeyDown(KeyCode.Mouse1))
        {
            qpressed = !qpressed;
            LR.enabled = false;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
            Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);
            Vector3 targ = transform.position;
            if (Physics.Raycast(ray, out hit))
            {
                targ = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
            Spell spell = SpellGenerator.CreateSpell("aaa", Random.Range(15, 20), 16, SpellType.PointTargetSpell, "Prefabs/PTS");
            SpellGenerator.SpawnSpell(spell, transform.position, transform.rotation, targ);
            qpassed = 0;
        }
        qpassed += Time.deltaTime;
        #endregion
        #region w
        if (Input.GetKeyDown(KeyCode.W) && wpassed >= wCD)
        {
            qpressed = false;
            epressed = false;
            LR.enabled = false;
            wpressed = !wpressed;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
        }
        if (wpressed)
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorTarg"), new Vector2(50, 50), CursorMode.Auto);
            Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Enemy" && hit.collider.gameObject.GetComponent<Renderer>().enabled == true)
                {
                    targGO = hit.collider.gameObject;
                    hasTarget = true;
                }
            }
        }
        if (wpressed && Input.GetKeyDown(KeyCode.Mouse1) && hasTarget && targGO.GetComponent<Renderer>().enabled == true)
        {
            wpressed = !wpressed;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
            Spell spell = SpellGenerator.CreateSpell("aaa", Random.Range(15, 20), 16, SpellType.EnemyTargetSpell, "Prefabs/ETS");
            SpellGenerator.SpawnSpell(spell, transform.position, transform.rotation, targGO);
            hasTarget = false;
            wpassed = 0;
        }
        wpassed += Time.deltaTime;

        #endregion
        #region e
        if (Input.GetKeyDown(KeyCode.E) && epassed >= eCD)
        {
            wpressed = false;
            qpressed = false;
            epressed = !epressed;
            LR.enabled = !LR.enabled;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
        }
        if (epressed)
        {
            Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);
            Vector3 targ = transform.position;
            if (Physics.Raycast(ray, out hit, 100, ~(1 << LayerMask.NameToLayer("SpellRaycastIgnore")), QueryTriggerInteraction.Ignore))
            {
                targ = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
            LR.enabled = true;
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, targ);
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorTarg"), new Vector2(50, 50), CursorMode.Auto);
        }
        if (epressed && Input.GetKeyDown(KeyCode.Mouse1))
        {
            epressed = !epressed;
            LR.enabled = false;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
            Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);
            Vector3 targ = transform.position;
            if (Physics.Raycast(ray, out hit))
            {
                targ = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
            Spell spell = SpellGenerator.CreateSpell("aaa", Random.Range(15, 150), 16, SpellType.AreaOfEffectSpell, "Prefabs/PTS", 5f);
            SpellGenerator.SpawnSpell(spell, transform.position, transform.rotation, targ, 5f);
            epassed = 0;

        }
        epassed += Time.deltaTime;
        #endregion
    }
}
