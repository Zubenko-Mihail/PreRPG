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
    private void Start()
    {
        UsefulThings.inputManager.Spells.CastFirstSpell.performed += _ => CastFirstSpell();
        UsefulThings.inputManager.Spells.CastSecondSpell.performed += _ => CastSecondSpell();
        UsefulThings.inputManager.Spells.CastThirdSpell.performed += _ => CastThirdSpell();
        UsefulThings.inputManager.Spells.RightMouseButton.performed += _ => RightMouseButtonPressed();
    }

    void Update()
    {
        if(qpassed < qCD)
            qpassed += Time.deltaTime;
        if(wpassed < wCD)
            wpassed += Time.deltaTime;
        if(epassed < eCD)
            epassed += Time.deltaTime;
        if (qpressed||epressed)
        {
            Ray ray = camComponent.ScreenPointToRay(UsefulThings.mouse.position.ReadValue());
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
        if (wpressed)
        {
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorTarg"), new Vector2(50, 50), CursorMode.Auto);
            Ray ray = camComponent.ScreenPointToRay(UsefulThings.mouse.position.ReadValue());
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Enemy" && hit.collider.gameObject.GetComponent<Renderer>().enabled == true)
                {
                    targGO = hit.collider.gameObject;
                    hasTarget = true;
                }
            }
        }
    }
    void CastFirstSpell()
    {
        if (qpassed >= qCD)
        {
            wpressed = false;
            epressed = false;
            qpressed = !qpressed;
            LR.enabled = !LR.enabled;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
        } 
    }
    void CastSecondSpell()
    {
        if (wpassed >= wCD)
        {
            qpressed = false;
            epressed = false;
            LR.enabled = false;
            wpressed = !wpressed;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
        }
    }
    void CastThirdSpell()
    {
        if (epassed >= eCD)
        {
            wpressed = false;
            qpressed = false;
            epressed = !epressed;
            LR.enabled = !LR.enabled;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
        }
    }
    void RightMouseButtonPressed()
    {
        if (qpressed)
        {
            qpressed = !qpressed;
            LR.enabled = false;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
            Ray ray = camComponent.ScreenPointToRay(UsefulThings.mouse.position.ReadValue());
            Vector3 targ = transform.position;
            if (Physics.Raycast(ray, out hit))
            {
                targ = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
            Spell spell = SpellGenerator.CreateSpell("aaa", Random.Range(15, 20), 16, SpellType.PointTargetSpell, "Prefabs/PTS");
            SpellGenerator.SpawnSpell(spell, transform.position, transform.rotation, targ);
            qpassed = 0;
        }
        if (epressed)
        {
            epressed = !epressed;
            LR.enabled = false;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
            Ray ray = camComponent.ScreenPointToRay(UsefulThings.mouse.position.ReadValue());
            Vector3 targ = transform.position;
            if (Physics.Raycast(ray, out hit))
            {
                targ = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
            Spell spell = SpellGenerator.CreateSpell("aaa", Random.Range(15, 150), 16, SpellType.AreaOfEffectSpell, "Prefabs/PTS", 5f);
            SpellGenerator.SpawnSpell(spell, transform.position, transform.rotation, targ, 5f);
            epassed = 0;
        }
        if (wpressed && hasTarget && targGO.GetComponent<Renderer>().enabled == true)
        {
            wpressed = !wpressed;
            Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/CursorDef"), Vector2.zero, CursorMode.Auto);
            Spell spell = SpellGenerator.CreateSpell("aaa", Random.Range(15, 20), 16, SpellType.EnemyTargetSpell, "Prefabs/ETS");
            SpellGenerator.SpawnSpell(spell, transform.position, transform.rotation, targGO);
            hasTarget = false;
            wpassed = 0;
        }

    }
}
