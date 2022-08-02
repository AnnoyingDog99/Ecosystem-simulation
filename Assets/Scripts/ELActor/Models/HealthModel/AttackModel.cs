using UnityEngine;
public class AttackModel : MonoBehaviour, IAttackModel
{
    [SerializeField] private float attackDamage;

    public float GetAttackDamage()
    {
        return this.attackDamage;
    }
}