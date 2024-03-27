using System.Collections.Generic;

using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Laserbeam : AttackProjectile
{
    LineRenderer lineRenderer;
    List<Collider2D> colliders = new();
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        lineRenderer.SetPosition(0, pos);
        RaycastHit2D raycast = Physics2D.Raycast(pos, _velosity, 999,_mask) ;
        lineRenderer.SetPosition(1, raycast.point!=null?raycast.point:pos+_velosity*99);
        if(raycast.collider != null&&!colliders.Contains(raycast.collider))
        {
            colliders.Add(raycast.collider);
            OnSomethingEnter2D(raycast.collider);
        }
    }
}
