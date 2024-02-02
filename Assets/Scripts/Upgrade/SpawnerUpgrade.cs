using UnityEngine;
using System;
using static SpawnerUpgrade;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
public class SpawnerUpgrade : MonoBehaviour
{
    [SerializeField] string upgradesJsonFilePath; // Путь к JSON файлу с данными прокачек
    void Start()
    {
        string json = Resources.Load<TextAsset>(upgradesJsonFilePath).text;
        UpgradeData[] upgrades = JsonUtility.FromJson<UpgradeData[]>(json);

        foreach(UpgradeData upgrade in upgrades)
        {
            GameObject obj;
            Upgrade _upgrade;
            if(Resources.Load<GameObject>(upgrade.prefabPath))
            {
                obj = Instantiate(Resources.Load<GameObject>(upgrade.prefabPath));
                _upgrade = obj.GetComponent<Upgrade>();
            }
            else
            {
                obj = Instantiate(new GameObject());
                _upgrade = obj.AddComponent(upgrade.classType) as Upgrade;
            }
            _upgrade.Sprite = upgrade.icon;
            _upgrade.ID = upgrade.id;
        }
    }
    GameObject Instantiate(GameObject obj)
    {
        return Instantiate(obj,transform);
    }
}
public class UpgradeData
{
    public string prefabPath;
    public Type classType;
    public Sprite icon;
    public int id;
}