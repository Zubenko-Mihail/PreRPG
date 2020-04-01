using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyConnector : MonoBehaviour
{
    bool isReloaded = true, canRotate = true, isTargetLocked = false;
    [HideInInspector]
    public bool canMove = true;
    public GameObject[] Targets;
    GameObject currTarget;
    public List<GameObject> listTargets = new List<GameObject>();
    public List<GameObject> listVisibleTargets = new List<GameObject>();
    public SphereCollider radiusCol, lookRadiusCol;
    Attack attackScript;
    Animator animator;
    NavMeshAgent nav;
    Vector3 lookTargetVector3;
    Quaternion lookDirectionTarget;
    public Enemy enemy = new Turret("turr", 12, 100);
    public string enemyType;
    RaycastHit hit;
    int layerMask = 1;
    float followTimeAfterLoosing = 1f, currTimeAfterLoosing = 100f;
    private void Awake()
    {
        layerMask |= (1 << LayerMask.NameToLayer("SpellRaycastIgnore")) | (1 << LayerMask.NameToLayer("Interactable"));
        layerMask = ~layerMask;
        layerMask |= (1 << 0);
        nav = GetComponent<NavMeshAgent>();
        attackScript = GetComponent<Attack>();
        animator = GetComponent<Animator>();
        if (enemyType == "Humanoid")
        {
            enemy = new Humanoid("Humanoid", 5, 100);
            UpdateAttackAnimBehaviours();
        }
    }
    public void GetDamage(Damage Damage)
    {
        enemy.GetDamage(Damage);
        if (enemy.HP <= 0) Death();
    }
    public void Start()
    {
        Targets = GameObject.FindGameObjectsWithTag("Player");
        gameObject.transform.Find("Radius").gameObject.AddComponent<SphereCollider>();
        gameObject.transform.Find("Radius").gameObject.AddComponent<EnemyTrigger>();
        radiusCol = gameObject.transform.Find("Radius").gameObject.GetComponent<SphereCollider>();
        radiusCol.isTrigger = true;
        radiusCol.radius = enemy.lookRange;



    }
    public void FixedUpdate()
    {
        if (enemy.enemyType == EnemyType.Humanoid)
        {
            if (nav.velocity.magnitude <= 0.1f)
                animator.SetBool("isRunning", false);
            else
                animator.SetBool("isRunning", true);
        }

        foreach (GameObject go in listTargets)
        {
            if (Physics.Raycast(enemy.enemyType == EnemyType.Humanoid ? transform.position + Vector3.up / 2 : transform.position, (go.transform.position + Vector3.up / 2 - transform.position).normalized, out hit, enemy.lookRange, layerMask))
            {
                if (hit.collider.gameObject == go && !listVisibleTargets.Contains(go))
                {
                    listVisibleTargets.Add(go);
                }
                else if (hit.collider.gameObject != go)
                {
                    listVisibleTargets.Remove(go);
                }
            }
        }

        if (listVisibleTargets.Count > 0)
        {
            currTimeAfterLoosing = 0;
            currTarget = ClosestTarget();
            lookTargetVector3 = currTarget.transform.position;
            lookTargetVector3.Set(lookTargetVector3.x, transform.position.y, lookTargetVector3.z);
            if (Vector3.Distance(currTarget.transform.position, transform.position) <= enemy.attackRange)
            {
                nav.isStopped = true;
                if (isTargetLocked && isReloaded)
                    Attack(currTarget);
            }
            else
            {
                if (canMove)
                {
                    nav.isStopped = false;
                    if (enemy.enemyType == EnemyType.Humanoid)
                        attackScript.RemoveTarget();
                }
                nav.SetDestination(currTarget.transform.position);
            }
        }
        else
        {
            if (followTimeAfterLoosing > currTimeAfterLoosing)
            {
                nav.SetDestination(currTarget.transform.position);
                currTimeAfterLoosing += Time.fixedDeltaTime;
            }
        }
        if (canRotate)
        {
            if (enemy.enemyType != EnemyType.Humanoid)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookTargetVector3 - transform.position), 1);
            }
            else if (listVisibleTargets.Count > 0 && (Vector3.Distance(currTarget.transform.position, transform.position) <= enemy.attackRange && Mathf.Abs(Quaternion.LookRotation(lookTargetVector3 - transform.position).eulerAngles.y - transform.rotation.eulerAngles.y) > 20f))
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookTargetVector3 - transform.position), 10);
            if (Mathf.Abs(Quaternion.LookRotation(lookTargetVector3 - transform.position).eulerAngles.y - transform.rotation.eulerAngles.y) <= 20f)
            {
                isTargetLocked = true;
            }
            else
                isTargetLocked = false;
        }
    }
    public void Attack(GameObject target)
    {
        if (enemy.enemyType == EnemyType.Humanoid)
            attackScript.AttackTarget(target);
        else
        {
            LineRenderer LR;
            LR = gameObject.AddComponent<LineRenderer>();
            LR.startColor = Color.red;
            LR.endColor = Color.blue;
            LR.material = new Material(Shader.Find("UI/Default"));
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, target.transform.position + Vector3.up / 2);
            isReloaded = false;
            StartCoroutine(AttackEffect(LR));
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1 / enemy.attackSpeed);
        isReloaded = true;
    }
    IEnumerator AttackEffect(LineRenderer LR)
    {
        canRotate = false;
        yield return new WaitForSeconds(1);
        canRotate = true;
        Destroy(LR);
    }
    GameObject ClosestTarget()
    {
        GameObject ret = listVisibleTargets[0];
        foreach (GameObject go in listVisibleTargets)
        {
            if (Vector3.Distance(go.transform.position, transform.position) < Vector3.Distance(ret.transform.position, transform.position))
                ret = go;
        }
        return ret;
    }
    void Death()
    {
        gameObject.AddComponent<Rigidbody>();
        nav.enabled = false;
        animator.enabled = false;
        gameObject.tag = "Untagged";
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        MeshCollider mc= gameObject.AddComponent<MeshCollider>();
        mc.sharedMesh = GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
        mc.convex = true;
        Destroy(gameObject, 20);
        enabled = false;
    }
    private void UpdateAttackAnimBehaviours()
    {
        foreach (AttackAnimBehaviour aAB in animator.GetBehaviours<AttackAnimBehaviour>())
        {
            aAB.attackSpeed = enemy.attackSpeed;
        }
    }
}