using System.Collections;
using UnityEngine;

public class Dasher : MonoBehaviour
{
    private Player _move;
    private PlayerAttack _playerAttack;
    private new Transform transform;
    private Rigidbody2D rb;

    private bool isDashEnd
    {
        set
        {
            _move.Stunned = value;
            _playerAttack.enabled = !value;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = base.transform;
        _move = GetComponent<Player>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private const float DashRange = 3.5f;

    public void Dash(float direction)
    {
        if (_playerAttack.Ammo <= 0)
        {
            return;
        }

        RaycastHit2D
            hit = Physics2D.Raycast(transform.position, Vector2.right * direction, DashRange, 1 << 10); // ��� �������
        if (hit)
        {
            transform.position = new Vector3(hit.point.x + -0.65f * direction * 0.5f, transform.position.y,
                transform.position.z); // ��� �����������
        }
        else
        {
            isDashEnd = true;
            transform.position += Vector3.right * direction * DashRange * 0.5f;
            StartCoroutine(DashEnd(direction));
        }
    }

    IEnumerator DashEnd(float direction)
    {
        Vector2 velocity = rb.velocity; //��� � �������� �������� ������
        for (float a = 0; a <= 10; a++)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = new Vector2(direction * (DashRange * 5 - a * DashRange / 2), 0); // ��� ��� ����������������
            yield return new WaitForFixedUpdate();
        }

        _playerAttack.Ammo--;
        isDashEnd = false;
        rb.velocity = velocity; //��� � ��������� ��������, ��� ����
    }
}