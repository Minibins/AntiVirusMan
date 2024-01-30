using UnityEngine;

public class Wall : MonoBehaviour
{
    public float TowerHeight = 0;
    [SerializeField] new Collider2D collider;
    private void Start()
    {
        TowerHeight =
            Physics2D.Raycast(collider.bounds.min.y * Vector3.up - Vector3.up + transform.position.x * Vector3.right,Vector2.down).
            collider.GetComponent<Wall>().TowerHeight++; TowerHeight += 1;
        if(TowerHeight >= 5)
            InstantiateWall.ClearWalls();
    }
}
