using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Resources : MonoBehaviour
{
    // Поле переменных
    public bool _isToPortal;
    public bool _isToPlayer = true;

    [SerializeField] private float _activationDistance = 5f;
    [SerializeField] private float _accelarationRate = .2f;
    [SerializeField] private float _moveSpeed = 0.5f;

    private Rigidbody2D _rb;

    private bool _isOpportunityToTake = true;
    private Vector2 _moveDir;
    // ----------------------------------

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isToPlayer)
            CheckDistance(transform.position, Player.Instance.transform.position);

        if (_isToPortal)
            CheckDistance(transform.position, PortalPlayer.Instance.transform.position);
    }

    private void FixedUpdate()
    {
        if (_isOpportunityToTake && _isToPlayer)
            MoveToObj();

        if (_isToPortal)
            MoveToObj();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isToPlayer && collision.transform.CompareTag("Player"))
        {
            sendingResourcesToInventory();
        }

        if (_isToPortal && collision.transform.CompareTag("PortalPlayer"))
        {
            addResourceToResourcePanel();
            Destroy(gameObject);
        }

    }

    // Поле приватных методов
    private void CheckDistance(Vector3 currentPos, Vector3 targetPos)
    {
        float distance = Vector3.Distance(currentPos, targetPos);

        if (distance < _activationDistance)
        {
            _moveDir = (targetPos - currentPos).normalized;
            _moveSpeed += _accelarationRate;
        }
        else
        {
            _moveDir = Vector2.zero;
            _moveSpeed = 0;
        }

    }

    private void MoveToObj()
    {
        _rb.MovePosition(_rb.position + _moveDir * (_moveSpeed * Time.fixedDeltaTime));
    }

    private void sendingResourcesToInventory()
    {
        bool hasTagEmpty = Inventory.Instance._items.Any(item => item.itemGameObj.CompareTag("Empty")); // Проверка на наличие в _items элемента с тегом Empty

        if (hasTagEmpty)
        {
            for (int i = 0; i < Inventory.Instance._maxCount; i++)
            {
                if (Inventory.Instance._items[i].itemGameObj.tag == "Empty")
                {
                    _isOpportunityToTake = true;
                    Destroy(gameObject);
                    if (DataBase.Instance.resourceMap.TryGetValue(transform.tag, out int indexItem))
                        Inventory.Instance.AddItem(DataBase.Instance._items[indexItem], 1, transform.tag);

                    break;
                }
            }
        }
        else
        {
            _isOpportunityToTake = false;
            Inventory.Instance.AddTextInventoryFull();
        }
    }

    private void addResourceToResourcePanel()
    {
        if (DataBase.Instance.resourceMap.ContainsKey(transform.tag))
            ResourcesPanel.Instance.AddCount(transform.tag);
    }
    // ----------------------------------
}
