using UnityEngine;

namespace Outpost.Player_ResourceTransfer
{
    public static class Player_ResourceTransfer
    {
        public static void ResourceTransfer()
        {
            if (Inventory.Instance != null)
            {
                for (int i = 0; i < Inventory.Instance._maxCount; i++)
                {

                    string tag = Inventory.Instance._items[i].itemGameObj.tag;
                    if (DataBase.Instance.resourceMap.ContainsKey(tag))
                    {
                        int resourceIndex = DataBase.Instance.resourceMap[tag];
                        for (int j = 1; j <= Inventory.Instance._items[i].count; j++)
                        {
                            GameObject obj = Object.Instantiate(DataBase.Instance._items[resourceIndex].obj, Player.Instance.transform);
                            SetupResources(obj);
                        }
                        DeleteResources(i);
                    }
                }
            }
        }

        private static void SetupResources(GameObject obj)
        {
            obj.GetComponent<Resources>()._isToPortal = true;
            obj.GetComponent<Resources>()._isToPlayer = false;
            obj.transform.localPosition = new Vector3(0, 1, 0);
        }

        private static void DeleteResources(int i)
        {
            Inventory.Instance.RemoveItem(i);
        }
    }
}

