using System;
using System.Collections;
using Unity.VisualScripting;
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
            ValueChanged?.Invoke(value);
            CoroutineRunner.instance.StartCoroutine(Reload());
        }
    }
    public float RechargeStep = 1, RechargeTime = 1;
    public delegate T ReloadInstruction<T>(float time);
    protected ReloadInstruction<object> reloadDelegate;
    private bool isOnReload;
    public RechargingValue(ValueBounds bounds, float value, float rechargeTime, float rechargeStep,
        ReloadInstruction<object> reload)
    {
        this.bounds = bounds;
        _value = value;
        RechargeTime = rechargeTime;
        reloadDelegate = reload;
        RechargeStep = rechargeStep;
    }
    private IEnumerator Reload()
    {
        if (!isOnReload && bounds.isInBounds(Value))
        {
            isOnReload = true;
            while (bounds.isInBounds(Value + RechargeStep))
            {
                yield return reloadDelegate(RechargeTime);
                Value += RechargeStep;
            }

            isOnReload = false;
        }
    }
    public static implicit operator int(RechargingValue value) => (int) value.Value;
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
    public static RechargingValue operator +(RechargingValue value, float increment)
    {
        value.Value += increment;
        return value;
    }
    public static RechargingValue operator -(RechargingValue value, float decrement)
    {
        value.Value -= decrement;
        return value;
    }
    public static RechargingValue operator *(RechargingValue value, float multiplier)
    {
        value.Value *= multiplier;
        return value;
    }
    public static RechargingValue operator /(RechargingValue value, float divisor)
    {
        value.Value /= divisor;
        return value;
    }
    public override string ToString() => Value.ToString();
}