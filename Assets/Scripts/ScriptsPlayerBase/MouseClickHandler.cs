using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    // Поле переменных
    private Vector2 clickPosition;
    private RaycastHit2D hit;
    // ----------------------------------

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("PortalPlayer"))
                    PanelPortal.Instance.PanelPortalOn();
                if (hit.collider.CompareTag("TownHall"))
                    PanelTownHall.Instance.PanelTownHallOn();
                if (hit.collider.CompareTag("Forge"))
                    PanelForge.Instance.PanelForgeOn();
                if (hit.collider.CompareTag("UnbuiltBarracks"))
                    PanelUnbuiltBarracks.Instance.PanelUnbuiltBarracksOn();
                if (hit.collider.CompareTag("Barracks"))
                    PanelBarracks.Instance.PanelBarracksOn();
                if (hit.collider.CompareTag("Barracks_2"))
                    PanelBarracks_2.Instance.PanelBarracksOn();
                if (hit.collider.CompareTag("Barracks_3"))
                    PanelBarracks_3.Instance.PanelBarracksOn();
                if (hit.collider.CompareTag("Barracks_4"))
                    PanelBarracks_4.Instance.PanelBarracksOn();
                if (hit.collider.CompareTag("Barracks_5"))
                    PanelBarracks_5.Instance.PanelBarracksOn();
                if (hit.collider.CompareTag("Barracks_6"))
                    PanelBarracks_6.Instance.PanelBarracksOn();
            }
        }
    }
}
