using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class CheckingCountMercenary : MonoBehaviour
{
    public static CheckingCountMercenary Instance;

    public int currentCount;

    private Text _countMercenary;


    private void Awake()
    {
        Instance = this;
        _countMercenary = GetComponent<Text>();

        for (int i = 1; i <= 6; i++)
        {
            if (GameData.mercenaryList[$"Mercenary_{i}"])
            {
                currentCount += 1;
            }
        }

        _countMercenary.text = currentCount.ToString();
    }
}
