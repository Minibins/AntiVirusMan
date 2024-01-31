using UnityEngine;
public class Wall : MonoBehaviour
{
    public float TowerHeight = 0;
    [SerializeField] new Collider2D collider;
    private void Start()
    {
        {
            RaycastHit2D hit = Physics2D.Raycast(collider.bounds.min.y * Vector3.up - Vector3.up + transform.position.x * Vector3.right,Vector2.down);
            Wall w;
            if(hit.collider.TryGetComponent<Wall>(out w))
                TowerHeight = w.TowerHeight++;
        }
        TowerHeight += 1;
        if(TowerHeight >= 5)
            InstantiateWall.ClearWalls();
    }
}