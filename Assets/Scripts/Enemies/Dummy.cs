using Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Dummy", order = 1)]
public class Dummy : Enemy
{
    public override void OnAfterDeserialize()
    {
        enemyType = EnemyType.Dummy;
    }
}



