using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TrainOption : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _currenderInterior;
    [SerializeField] private SpriteRenderer[] _currenderwheels;
    [SerializeField] private GameObject _grid;

    public void ChangeTrain(Tilemap _tilemap, Sprite _interior, Sprite _wheel1, Sprite _wheel2, Sprite _wheel3,
        Sprite _wheel4)
    {
        _currenderInterior.sprite = _interior;

        Tilemap obj = Instantiate(_tilemap, _grid.transform);
        obj.transform.localPosition = Vector3.zero;

        _currenderwheels[0].sprite = _wheel1;
        _currenderwheels[1].sprite = _wheel2;
        _currenderwheels[2].sprite = _wheel3;
        _currenderwheels[3].sprite = _wheel4;
    }
}