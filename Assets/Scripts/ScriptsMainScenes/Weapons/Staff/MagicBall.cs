using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    public float moveSpeed = 5f;
    public float lifeTime = 3f;

    private int damage;
    private Transform target;

    private float timer;
    private bool hasReachedTarget = false;
    private Vector2 directionAttack;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        directionAttack = (target.position - transform.position).normalized;
        timer = lifeTime;

        MoveTowardsTarget();
    }

    private void Update()
    {
        if (!hasReachedTarget && timer > 0)
        {
            timer -= Time.deltaTime;
            MoveTowardsTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void MoveTowardsTarget()
    {
        rb.MovePosition(rb.position + directionAttack * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Allie") || collision.CompareTag("PortalPlayer"))
        {
            collision.GetComponent<IDamageable>().TakeDamage(damage, transform);
            hasReachedTarget = true;
        }
    }
}
