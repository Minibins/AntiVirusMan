using System.Collections;

using UnityEngine;
public class ExplodeAndDeath : MonoBehaviour, IScannable
{
    [SerializeField] private float chargeTime = 2f;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private int _explosionPower;
    [SerializeField] private bool DestroyHimself = true, OnScan = true;

    public void Action()
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(chargeTime);
            Explosion();
        }
    }

    public void EndScan()
    {
        if (OnScan)
            Explosion();
    }

    public void StartScan()
    {
    }

    public void StopScan()
    {
    }

    protected virtual GameObject Explosion()
    {
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        IExplosion Explosion;
        if(explosion.TryGetComponent<IExplosion>(out Explosion))
        {
            Explosion.Radius = _explosionRadius;
            Explosion.Power = _explosionPower;
        }
        Destroy();
        return explosion;
    }

    protected void Destroy()
    {
        if(DestroyHimself)
            Destroy(gameObject);
    }
}