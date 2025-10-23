using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AlliesAI))]

public class MercenaryVisual : MonoBehaviour
{
    // Поле переменных
    private AlliesAI _alliesAI;
    private Animator _animator;

    private const string IS_RUNNING = "isRunning";
    // ----------------------------------

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _alliesAI = GetComponent<AlliesAI>();
    }

    private void Update()
    {
        _animator.SetBool(IS_RUNNING, _alliesAI.isEnemyRunning);
    }
}
