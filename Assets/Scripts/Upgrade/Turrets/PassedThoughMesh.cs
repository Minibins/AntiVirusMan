using System.Collections.Generic;
using UnityEngine;

public class PassedThoughMesh : MonoBehaviour
{

    private static List<PassedThoughMesh> all = new();
    private int exp = 3;
    public int Exp { get => exp; private set => exp = Mathf.Clamp(value,0,3); }
    public void ReduceEXP() => Exp-=2;
    private void Awake()=>all.Add(this);
    private void OnDestroy()=>all.Remove(this);
    public static void ObjectPassedMesh()
    {
        foreach(PassedThoughMesh p in all)
            p.Exp++;
    }
}