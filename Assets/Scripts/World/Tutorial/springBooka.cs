using System.Collections.Generic;
using DustyStudios;
using DustyStudios.MathAVM;
using UnityEngine;

public class springBooka : BookaCollisionChecker
{
    private bool UnderPreasure;
    private Rigidbody2D _rigidbody;
    private Vector2 defaultPosition;
    [SerializeField] private float maxOffset, rotationOfVelocity, VelocityStrengh;

    private void Start()
    {
        defaultPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        EnterAction += SetUnderPreasureTrue;
        ExitAction += SetUnderPreasureFalse;
    }

    private void SetUnderPreasureTrue()
    {
        UnderPreasure = true;
    }

    private void SetUnderPreasureFalse()
    {
        UnderPreasure = false;
    }

    private void FixedUpdate()
    {
        if (UnderPreasure)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            if (_rigidbody.position.y < defaultPosition.y - maxOffset)
            {
                UnderPreasure = false;
                foreach (GameObject g in EnteredThings)
                {
                    g.GetComponent<Rigidbody2D>().velocity =
                        MathA.RotatedVector(Vector2.up * VelocityStrengh, rotationOfVelocity);
                }

                const string ppname = "EnemyBookaTutorial";
                if (!DustyConsoleInGame.UsedConsoleInSession && EnteredThings.Count > 0)
                    PlayerPrefs.SetInt(ppname, PlayerPrefs.GetInt(ppname, 0) + 1);
            }
        }

        if (!UnderPreasure)
        {
            _rigidbody.bodyType = RigidbodyType2D.Static;
            transform.position = defaultPosition;
        }
    }

    protected override bool EnterCondition(Collider2D other)
    {
        Rigidbody2D rigidbody;
        return base.EnterCondition(other) && other.gameObject.TryGetComponent<Rigidbody2D>(out rigidbody);
    }

    protected override bool ExitCondition(Collider2D other)
    {
        List<GameObject> EnteredThingsWithoutExiter = EnteredThings;
        if (EnteredThingsWithoutExiter.Contains(other.gameObject))
        {
            EnteredThingsWithoutExiter.Remove(other.gameObject);
        }

        return EnteredThingsWithoutExiter.Count <= 0;
    }
}