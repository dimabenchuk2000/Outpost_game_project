using System.Linq;

public static class ResetProgress
{
    public static void ResetAll()
    {
        GameData.countBarracks = 1;
        GameData.globalHP = 3;

        foreach (string key in GameData.resourceCounts.Keys.ToList())
        {
            GameData.resourceCounts[key] = 0;
        }

        foreach (string key in GameData.buildingsLevel.Keys.ToList())
        {
            GameData.buildingsLevel[key] = 1;
        }

        foreach (string key in GameData.improvementList.Keys.ToList())
        {
            GameData.improvementList[key] = false;
        }

        foreach (string key in GameData.weaponLevel.Keys.ToList())
        {
            GameData.weaponLevel[key] = 1;
        }

        foreach (string key in GameData.mercenaryList.Keys.ToList())
        {
            GameData.mercenaryList[key] = false;
        }

        GameData.mercenaryList["Mercenary_1"] = true;
    }
}
