using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ExitFromTheCave : MonoBehaviour
{
    // Поле переменных
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    [SerializeField] private BoxCollider2D _cameraConfinerMain;
    [SerializeField] private GameObject _caveEntrance;
    // ----------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {

            Player.Instance._isPlayerMove = false;
            ScreenDarken.Instanse.DarkenScreen();

            StartCoroutine(PlayerMovementWorld(player));
        }
    }

    // Поле корутин
    IEnumerator PlayerMovementWorld(Player player)
    {
        yield return new WaitForSeconds(2f);

        var confiner = _cinemachineCamera.GetComponent<CinemachineConfiner2D>();
        confiner.BoundingShape2D = _cameraConfinerMain;

        player.transform.position = _caveEntrance.transform.position;
        player.transform.position -= new Vector3(0, 3, 0);

        StartCoroutine(LightenScreen());
    }

    IEnumerator LightenScreen()
    {
        yield return new WaitForSeconds(1f);

        LightController.Instanse._isPlayerInCave = false;
        LightController.Instanse.CheckingPlayerLocation();

        Player.Instance._isPlayerMove = true;
        ScreenDarken.Instanse.LightenScreen();
    }
    // ----------------------------------
}
