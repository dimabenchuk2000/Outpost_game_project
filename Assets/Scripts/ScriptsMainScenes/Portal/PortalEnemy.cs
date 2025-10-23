using UnityEngine;

public class PortalEnemy : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private GameObject _objectToSpawn; // объект для создания
    [SerializeField] private float _spawnInterval = 5f; // интервал между спавнами в секундах
    [SerializeField] private float _spawnDuration = 30f; // общий период спавна в секундах
    [SerializeField] private float _spawnDistance = 2f; // расстояние от объекта для спавна

    private float _timer = 0f;
    private float _elapsedTime = 0f;

    private bool _isSpawnEnemy = false;
    // ----------------------------------

    private void Update()
    {
        if (_elapsedTime < _spawnDuration)
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {
                SpawnObject();
                _timer = 0f;
            }
            _elapsedTime += Time.deltaTime;
        }
    }

    public bool IsSpawnEnemy() => _isSpawnEnemy;

    // Поле приватных методов
    private void SpawnObject()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _spawnDistance;
        GameObject newObjectToSpwan = Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity);

        Animator _objectToSpawnAnimator = newObjectToSpwan.GetComponentInChildren<Animator>();

        if (_objectToSpawnAnimator != null)
            _objectToSpawnAnimator.SetTrigger("isSpawn");
    }
    // ----------------------------------
}

