using UnityEngine;
using Entities;

[CreateAssetMenu(menuName = "Enemy/Humanoid", order = 1)]
public class Humanoid : Enemy
{
    public override void OnAfterDeserialize()
    {
        enemyType = EnemyType.Humanoid;
    }
}



