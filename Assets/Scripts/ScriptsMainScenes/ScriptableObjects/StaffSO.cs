using UnityEngine;

[CreateAssetMenu(fileName = "StaffSO", menuName = "Scriptable Objects/StaffSO")]
public class StaffSO : ScriptableObject
{
    public string staffName;
    public float staffAttackRate;
    public int staffDamage;
}
