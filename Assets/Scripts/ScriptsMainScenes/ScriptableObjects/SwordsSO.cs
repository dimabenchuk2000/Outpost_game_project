using UnityEngine;

[CreateAssetMenu(fileName = "SwordsSO", menuName = "Scriptable Objects/SwordsSO")]
public class SwordsSO : ScriptableObject
{
    public string swordName;
    public float swordAttackRate;
    public int swordDamage;
    public float swordSpeedAttack;
}
