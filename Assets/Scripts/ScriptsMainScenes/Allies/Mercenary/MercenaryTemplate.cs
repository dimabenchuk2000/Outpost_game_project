using UnityEngine;

public class MercenaryTemplate : MonoBehaviour
{
    [SerializeField] private GameObject[] swords;
    [SerializeField] private GameObject mercenary;

    private int _swordLevel;
    private Vector3 _mousePosition;

    private void Update()
    {
        _mousePosition = GameInput.Instance.GetMousePosition();
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        transform.position = new Vector3(
            _mousePosition.x,
            _mousePosition.y,
            transform.position.z);

        if (Input.GetMouseButtonDown(0) && PanelCreateMercenary.Instance.isCreateTemplate)
        {
            PanelCreateMercenary.Instance.isCreateTemplate = false;
            _swordLevel = PanelCreateMercenary.Instance.MercenarySwordLevel();
            Vector3 mousePositionScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject newMercenary = Instantiate(mercenary, new Vector3(mousePositionScreen.x, mousePositionScreen.y, 0), Quaternion.identity);
            Instantiate(swords[_swordLevel - 1], newMercenary.transform);

            PanelCreateMercenary.Instance.allies.Add(newMercenary);

            Destroy(gameObject);
        }
    }
}
