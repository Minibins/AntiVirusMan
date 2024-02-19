using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] int rows, columns, edgeHeight, centerHeight;
    [SerializeField] Vector2Int stairSize;
    [SerializeField] float cubeSize = 1.0f;
    List<GameObject> gameObjects = new();
    private bool[,] boolArray;
    int AbsoluteStairSize
    {
        get => (stairSize.x + stairSize.y) / 2;
    }
    int AbsoluteStairHeight
    {
        get => (edgeHeight - centerHeight) / AbsoluteStairSize;
    }
    void Start()
    {
        boolArray = new bool[rows,columns];
        for(int i = 0; i < rows / AbsoluteStairSize;)
        {
            int height = (int)math.abs((float)AbsoluteStairHeight * (((float)i - (float)rows/2f) ));
            for(int x = 0; x < AbsoluteStairSize;)
                try
                {
                    boolArray[i * AbsoluteStairSize + x++,height] = true;
                }
                catch
                {
                    DustyStudios.DustyConsole.Print("X=" + (i * AbsoluteStairSize + x) + "    Y=" + height);
                }
            i++;
        }

        CreateCubesFromBoolArray();
    }
    private void OnValidate()
    {
        foreach(GameObject obj in gameObjects) UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(obj,true);
        gameObjects.Clear();
        Start();
    }
    void CreateCubesFromBoolArray()
    {
        for(int x = 0; x < rows; x++)
        {
            for(int y = 0; y < columns; y++)
            {
                if(boolArray[x,y])
                {
                    Vector3 cubePosition = new Vector2(x,y)*cubeSize;
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = cubePosition;
                    gameObjects.Add(cube);
                }
            }
        }
    }
}