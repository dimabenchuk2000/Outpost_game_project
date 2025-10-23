using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CaveEntrance : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    [SerializeField] private BoxCollider2D _cameraConfinerCave;
    [SerializeField] private Vector3[] spawnPoints = new Vector3[4];
    // ----------------------------------

    private void Start()
    {
        int index = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[index];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            Player.Instance._isPlayerMove = false;
            ScreenDarken.Instanse.DarkenScreen();

            StartCoroutine(PlayerMovementCave(player));


        }
    }

    // Поле корутин
    IEnumerator PlayerMovementCave(Player player)
    {
        yield return new WaitForSeconds(2f);

        var confiner = _cinemachineCamera.GetComponent<CinemachineConfiner2D>();
        confiner.BoundingShape2D = _cameraConfinerCave;

        player.transform.position = new Vector3(111, -25, 0);

        StartCoroutine(LightenScreen());

    }

    IEnumerator LightenScreen()
    {
        yield return new WaitForSeconds(1f);

        LightController.Instanse._isPlayerInCave = true;
        LightController.Instanse.CheckingPlayerLocation();

        Player.Instance._isPlayerMove = true;
        ScreenDarken.Instanse.LightenScreen();
    }
    // ----------------------------------
}
