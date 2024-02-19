using Unity.Mathematics;

using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] int rows, columns, edgeHeight, centerHeight;
    [SerializeField] Vector2Int stairSize;
    [SerializeField] float cubeSize = 1.0f; 

    private bool[,] boolArray;
    int AbsoluteStairSize
    {
        get => (stairSize.x + stairSize.y) / 2;
    }
    void Start()
    {
        boolArray = new bool[rows,columns];
        for(int i = 0; i < rows/ AbsoluteStairSize; )
        {
            int height = math.abs((edgeHeight-centerHeight)*((rows/2)+((i+1)*AbsoluteStairSize)));
            for(int x = 0; x < AbsoluteStairSize;)
                try
                {
                    boolArray[i * AbsoluteStairSize + x++,height] = true;
                }
                catch
                {
                    DustyStudios.DustyConsole.Print("X="+ (i * AbsoluteStairSize + x) + "    Y="+height);
                }
            i++;
        }

        CreateCubesFromBoolArray();
    }
    void CreateCubesFromBoolArray()
    {
        for(int x = 0; x < rows; x++)
        {
            for(int y = 0; y < columns; y++)
            {
                if(boolArray[x,y])
                {
                    Vector3 cubePosition = new Vector3(x * cubeSize, y * cubeSize, 0);
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = cubePosition;
                }
            }
        }
    }
}