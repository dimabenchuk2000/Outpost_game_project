using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Environment : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private float _shakeDuration = 0.2f;
    [SerializeField] private float _shakeMagnitude = 0.1f;
    [SerializeField] private GameObject _destroyParticle;
    [SerializeField] private Sprite _destroyObjImg;
    [SerializeField] private GameObject _resourcesEnvironment;

    private SpriteRenderer _spriteRenderer;

    private int _maxHealth = 3;
    private int _currentHealth;
    private Vector3 _originalPosition;
    private float _shakeTime;
    private bool _isShaking = false;
    private bool _isTreeDestroy = false;
    // ----------------------------------

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _originalPosition = transform.localPosition;
    }

    // Поле публичных методов
    public void TakeDamage(int damage)
    {
        if (!_isTreeDestroy)
        {
            if (!_isShaking)
                StartCoroutine(Shake());

            _currentHealth -= damage;
            DetectDestroy();
        }

    }
    // ----------------------------------

    // Поле приватных методов
    private void DetectDestroy()
    {
        if (_currentHealth <= 0)
        {
            GameObject destroyParticle = Instantiate(_destroyParticle, transform);
            destroyParticle.transform.localPosition = new Vector3(0, 2, 0);
            destroyParticle.GetComponent<ParticleSystem>().Play();
            Destroy(destroyParticle, 1f);

            _spriteRenderer.sprite = _destroyObjImg;

            GameObject resources_1 = Instantiate(_resourcesEnvironment, transform);
            GameObject resources_2 = Instantiate(_resourcesEnvironment, transform);
            GameObject resources_3 = Instantiate(_resourcesEnvironment, transform);

            resources_1.transform.localPosition = new Vector3(0, 0, 0);
            resources_2.transform.localPosition = new Vector3(0, 1, 0);
            resources_3.transform.localPosition = new Vector3(0, 2, 0);

            _isTreeDestroy = true;
        }
    }
    // ----------------------------------

    // Поле корутин
    IEnumerator Shake()
    {
        _isShaking = true;
        _shakeTime = 0f;

        while (_shakeTime < _shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * _shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * _shakeMagnitude;

            transform.localPosition = _originalPosition + new Vector3(offsetX, offsetY, 0);

            _shakeTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _originalPosition;
        _isShaking = false;
    }
    // ----------------------------------
}
