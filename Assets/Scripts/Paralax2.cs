using UnityEngine;

public class Paralax2 : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _endX;
    [SerializeField] private float _startX;

    private void Update()
    {
        transform.Translate((Vector2.left * _speed * Time.deltaTime));

        if (transform.position.x >= _endX)
        {
            Vector2 pos = new Vector2(_startX, transform.position.y);
            transform.position = pos;
        }
    }
}
