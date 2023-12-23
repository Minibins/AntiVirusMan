using UnityEngine;
using System.Collections;

class Dasher : MonoBehaviour
{
    Player _move;
    PlayerAttack _playerAttack;
    new Transform transform;
    private bool isDashEnd
    {
        set
        {
            _move.Stunned = value;
            _playerAttack.enabled = !value;
            _move.enabled = !value;
        }
    }
    private void Start()
    {
        transform = base.transform;
        _move = GetComponent<Player>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    [SerializeField] public float dashRange;
    public void Dash(int direction)
    {
        {
            var Ammo = _playerAttack.Ammo;
            if(Ammo < 0)
            {
                return;
            }
            Ammo--;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, dashRange, 1 << 10); // Тут рейкаст
        if(hit)
        {
            transform.position = new Vector3(hit.point.x + -0.65f * direction * 0.5f,transform.position.y,
                transform.position.z); // Тут перемещение
        }
        else
        {
            StartCoroutine(DashEnd(direction));
            transform.position += Vector3.right * direction * dashRange * 0.5f;
        }
        IEnumerator DashEnd(int direction)
        {
            isDashEnd = true;
            for(int a = 0; a <= 10; a++)
            {
                _move.MoveBoth(new Vector2(direction * (dashRange * 5 - a / 2 * dashRange) * 10,0));
                yield return new WaitForFixedUpdate();
            }
            isDashEnd = false;
        }
    }
}