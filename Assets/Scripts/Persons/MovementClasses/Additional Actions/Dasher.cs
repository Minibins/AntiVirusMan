using UnityEngine;
using System.Collections;

class Dasher : MonoBehaviour
{
    Player _move;
    PlayerAttack _playerAttack;
    new Transform transform;
    Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
        transform = base.transform;
        _move = GetComponent<Player>();
        _playerAttack = GetComponent<PlayerAttack>();
    }
    const float DashRange = 3.5f;
    public void Dash(float direction)
    {
        if(_playerAttack.Ammo <= 0)
        {
            return;
        }
        _playerAttack.Ammo--;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, DashRange, 1 << 10); // Тут рейкаст
        if(hit)
        {
            transform.position = new Vector3(hit.point.x + -0.65f * direction * 0.5f,transform.position.y,transform.position.z); // Тут перемещение
        }
        else
        {
            transform.position += Vector3.right * direction * DashRange * 0.5f;
            
            StartCoroutine(DashEnd(direction));
        }
        
    }
    IEnumerator DashEnd(float direction)
    {
        isDashEnd = true;
        for(float a = 0; a <= 10; a++)
        {
            rb.velocity=new Vector2(direction * (DashRange * 5 - a / 2 * DashRange),0);
            yield return new WaitForFixedUpdate();
        }
        isDashEnd = false;
    }
}