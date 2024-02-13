using System.Collections;
using UnityEngine;

public class AbstractAura : TagCollisionChecker
{
    [SerializeField] protected float _ReloadTime;
    private bool _reloadNow = false;
    private void Start()
    {
        StayAction += () => StartCoroutine(SlowCoroutine());
    }
    protected override bool StayCondition(Collider2D other) => !_reloadNow && base.StayCondition(other);
    private IEnumerator SlowCoroutine()
    {
        _reloadNow = true;
        yield return AuraAction();
        _reloadNow = false;
    }
    protected virtual IEnumerator AuraAction() => null;
}
