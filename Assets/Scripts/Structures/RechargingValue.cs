using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class RechargingValue
{
    public event Action<float> ValueChanged;
    public ValueBounds bounds;
    private float _value;
    public float Value
    {
        get { return _value; }
        set
        {
            _value = bounds.Clamp(value);
            ValueChanged?.Invoke(_value);
            CoroutineRunner.instance.StartCoroutine(Reload());
        }
    }
    public float RechargeTime=1, RechargeStep=1;
    bool isOnReload;

    public RechargingValue(ValueBounds bounds,float value,float rechargeTime,float rechargeStep)
    {
        this.bounds = bounds;
        _value = value;
        RechargeTime = rechargeTime;
        RechargeStep = rechargeStep;
    }

    private IEnumerator Reload()
    {
        if(!isOnReload && bounds.isInBounds(Value))
        {
            isOnReload = true;
            yield return new WaitForSeconds(RechargeTime);
            while(bounds.isInBounds(Value + RechargeStep,false))
            {
                Value += RechargeStep;
                yield return new WaitForSeconds(RechargeTime);
            }
            Value = RechargeStep > 0 ? bounds.max : bounds.min;
            isOnReload = false;
        }
    }
    public static implicit operator int(RechargingValue value) => (int)value.Value;
    public static implicit operator float(RechargingValue value) => value.Value;
    public static RechargingValue operator ++(RechargingValue value)
    {
        value.Value += 1;
        return value;
    }
    public static RechargingValue operator --(RechargingValue value)
    {
        value.Value -= 1;
        return value;
    }
    public static RechargingValue operator +(RechargingValue value,float increment)
    {
        value.Value += increment;
        return value;
    }

    public static RechargingValue operator -(RechargingValue value,float decrement)
    {
        value.Value -= decrement;
        return value;
    }

    public static RechargingValue operator *(RechargingValue value,float multiplier)
    {
        value.Value *= multiplier;
        return value;
    }

    public static RechargingValue operator /(RechargingValue value,float divisor)
    {
        value.Value /= divisor;
        return value;
    }
}