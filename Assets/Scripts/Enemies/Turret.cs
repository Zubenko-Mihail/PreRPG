using UnityEngine;
using Entities;

[CreateAssetMenu(menuName = "Enemy/Turret", order = 2)]

public class Turret : Enemy 
{
    public override void OnAfterDeserialize()
    {
        enemyType = EnemyType.Turret;
    }
}



