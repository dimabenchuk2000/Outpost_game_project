using UnityEngine;

[CreateAssetMenu(fileName = "ToolsSO", menuName = "Scriptable Objects/ToolsSO")]
public class ToolsSO : ScriptableObject
{
    public string toolsName;
    public float toolsExtractionRate;
    public int toolsDamage;
    public float toolsSpeedExtraction;
}
