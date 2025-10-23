using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private float speed = 15;
    private Vector3 _vectorDirectionMovement;
    // ----------------------------------

    private void Update()
    {
        _vectorDirectionMovement = GameInput.Instance.GetVectorDirectionMovement();
    }

    private void FixedUpdate()
    {
        transform.Translate(_vectorDirectionMovement * (speed * Time.fixedDeltaTime));
    }
}
