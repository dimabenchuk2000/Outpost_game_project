using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public static CoroutineRunner Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Coroutine RunCoroutine(IEnumerator routine)
    {
        if (routine == null)
        {
            Debug.LogError("Рутина равна null!");
            return null;
        }
        return base.StartCoroutine(routine);
    }
}
