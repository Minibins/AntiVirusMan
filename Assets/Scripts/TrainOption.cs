using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrainOption : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _currenderInterior;
    [SerializeField] private SpriteRenderer[] _currenderwheels;

    private void Start()
    {
    }

    public void ChangeTrain(Sprite _interior, Sprite _wheel1, Sprite _wheel2, Sprite _wheel3, Sprite _wheel4)
    {
        _currenderInterior.sprite = _interior;

            _currenderwheels[0].sprite = _wheel1;
            _currenderwheels[1].sprite = _wheel2;
            _currenderwheels[2].sprite = _wheel3;
            _currenderwheels[3].sprite = _wheel4;
        
    }
}