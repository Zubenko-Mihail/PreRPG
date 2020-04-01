using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator animator;
    Inventory inventory;
    Controls controls;
    EnemyConnector enemyConnector;
    float currAnimLength;
    [HideInInspector]
    public GameObject target;
    bool isAttackAnimationPlaying = false, waitUntilRemoveTarget = false, canStopAnim = true, searchNextTarget = false, isOnPlayer = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        SetAttackAnimBehaviours();
        if(GetComponent<Controls>())
        {
            isOnPlayer = true;
            controls = GetComponent<Controls>();
            inventory = GetComponent<Inventory>();
        }
        else
        {
            isOnPlayer = false;
            enemyConnector = GetComponent<EnemyConnector>();
        }
    }

    public void AttackTarget(GameObject go)
    {
        target = go;
        if (isOnPlayer)
        {
            if ((Weapon)inventory.Weapon_1.Item != null)
            {
                animator.SetInteger("AttackNumber", Random.Range(0, 6));
                animator.SetTrigger("Attack");

            }
        }
        else
        {
            animator.SetInteger("AttackNumber", Random.Range(0, 6));
            animator.SetTrigger("Attack");
        }
    }
    public void GiveDamage()
    {
        if (isOnPlayer)
        {
            if (target != null)
            {
                target.GetComponent<EnemyConnector>().GetDamage(new Damage(((Weapon)inventory.Weapon_1.Item).GetDamage()));
                controls.canInteract = false;
            }
        }
        else
        {
            if (target != null)
            {
                target.GetComponent<PlayerStats>().GetDamage(new Damage(enemyConnector.enemy.damage));
                enemyConnector.canMove = false;
            }
        }
        canStopAnim = false;

    }
    public void AttackAnimBeginning()
    {
        isAttackAnimationPlaying = true;
        searchNextTarget = true;
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("ExitAttackAnim");
    }
    public void AttackAnimEnded()
    {
        isAttackAnimationPlaying = false;
        canStopAnim = true;
        if (waitUntilRemoveTarget)
        {
            target = null;
            waitUntilRemoveTarget = false;
            searchNextTarget = false;
        }
        if (isOnPlayer)
        {
            controls.canInteract = true;
            if (target != null)
            {
                controls.waitUntilInteract = StartCoroutine(controls.WaitUntilInteract(target));
            }
            else if (searchNextTarget)
            {
                Collider[] colArr = Physics.OverlapSphere(transform.position, 6, (1 << LayerMask.NameToLayer("SpellRaycastIgnore")));
                float minDis = 99999, dis;
                GameObject go = null;
                foreach (Collider col in colArr)
                {
                    dis = Vector3.Distance(col.transform.position, transform.position);
                    if (dis < minDis)
                    {
                        minDis = dis;
                        go = col.gameObject;
                    }
                    if (go != null)
                        controls.waitUntilInteract = StartCoroutine(controls.WaitUntilInteract(go));
                }
            }
        }
        else
        {
            enemyConnector.canMove = true;
        }
    }
    public void RemoveTarget()
    {
        if (canStopAnim && isAttackAnimationPlaying)
        {
            searchNextTarget = false;
            target = null;
            animator.SetTrigger("ExitAttackAnim");
        }
        else if (isAttackAnimationPlaying)
            waitUntilRemoveTarget = true;
    }
    private void SetAttackAnimBehaviours()
    {
        Attack aS = this;
        foreach (AttackAnimBehaviour aAB in animator.GetBehaviours<AttackAnimBehaviour>())
        {
            aAB.attackScript = aS;
        }
    }
}
