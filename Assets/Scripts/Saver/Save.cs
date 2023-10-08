using UnityEngine;
using System.IO;

public class Save : MonoBehaviour
{
    public Data data;

    private void Awake()
    {
        LoadField();
    }
    public void LoadField()
    {
        data = JsonUtility.FromJson<Data>(File.ReadAllText(Application.streamingAssetsPath + "/Data.avm"));
    }

    public void SaveField()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/Data.avm", JsonUtility.ToJson(data));
    }
}
