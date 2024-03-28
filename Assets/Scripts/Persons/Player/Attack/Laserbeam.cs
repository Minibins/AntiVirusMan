using System.Collections.Generic;

using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Laserbeam : AttackProjectile
{
    LineRenderer lineRenderer;
    [SerializeField] float range,maxOffset;
    float currentRange = 0f;
    List<Collider2D> colliders = new();
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    override protected void Start()
    {
        _velosity.Normalize();
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        currentRange = ((currentRange)*2 + range * Damage) / 3;
        _velosity = new Vector2(_velosity.x+Random.Range(-maxOffset,maxOffset),_velosity.y+ Random.Range(-maxOffset,maxOffset)).normalized;
        lineRenderer.SetPosition(0, pos);
        RaycastHit2D raycast = Physics2D.Raycast(pos, _velosity, currentRange,_mask) ;
        lineRenderer.SetPosition(1, raycast.point!=Vector2.zero?raycast.point:pos+_velosity*currentRange);
        if(raycast.collider != null&&!colliders.Contains(raycast.collider))
        {
            colliders.Add(raycast.collider);
            IScannable[] scannables = raycast.collider.GetComponents<IScannable>();
            if(scannables.Length!=0)
            {
                foreach (var scannable in scannables)
                    scannable.StartScan();
            }
        }
    }
}
