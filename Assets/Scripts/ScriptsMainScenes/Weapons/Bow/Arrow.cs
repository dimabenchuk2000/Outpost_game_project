using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Arrow : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private float _speedArrow = 15f;
    [SerializeField] private ArrowsSO _arrowsSO;

    private Rigidbody2D _rb;
    private Vector3 _mousePos;
    private Vector2 _vectorDirectionAttack;
    private float _angleArrow;
    private int _arrowDamage;
    // ----------------------------------

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(DestroyArrow());

        _arrowDamage = _arrowsSO.arrowDamage;

        // Определяет в какую сторону полетит стрела
        _mousePos = GameInput.Instance.GetVectorDirectionToMouse();
        _vectorDirectionAttack = (_mousePos - transform.position).normalized;
        _angleArrow = Mathf.Atan2(_vectorDirectionAttack.y, _vectorDirectionAttack.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angleArrow));
        // ----------------------------------
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _vectorDirectionAttack * (_speedArrow * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(_arrowDamage, Player.Instance.transform);
            Destroy(transform.gameObject);
        }
    }

    // Поле корутин
    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(1f);
        Destroy(transform.gameObject);
    }
    // ----------------------------------
}
