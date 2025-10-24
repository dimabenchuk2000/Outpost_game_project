using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public static CoroutineRunner Instance;

    private Dictionary<string, Coroutine> _coroutines = new Dictionary<string, Coroutine>();

    private void Awake()
    {
        Instance = this;
    }

    public Coroutine RunCoroutine(string name, IEnumerator routine)
    {
        if (routine == null)
        {
            Debug.LogError("Рутина равна null!");
            return null;
        }

        if (_coroutines.ContainsKey(name))
        {
            Debug.LogWarning($"Корутина с именем {name} уже существует!");
            return null;
        }

        Coroutine coroutine = base.StartCoroutine(routine);
        _coroutines[name] = coroutine;
        return coroutine;
    }

    public void StoppedCoroutine(string name)
    {
        if (_coroutines.TryGetValue(name, out Coroutine coroutine))
        {
            base.StopCoroutine(coroutine);
            _coroutines.Remove(name);
        }
        else
        {
            Debug.LogWarning($"Корутина с именем {name} не найдена!");
        }
    }
}
