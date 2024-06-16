using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MultiRechargingValue : RechargingValue
{
    public List<RechargeStream> rechargeStreams;
    private ReloadInstruction<object> reload;
    public MultiRechargingValue(ValueBounds bounds,float value,List<(float step, float rechargeTime)> rechargeStreams,ReloadInstruction<object> reload) : base(bounds,value,rechargeStreams[0].rechargeTime,rechargeStreams[0].step,null)
    {
        this.rechargeStreams = rechargeStreams.Where(s => s.rechargeTime>0).Select(val=>new RechargeStream(val.step,val.rechargeTime,val.rechargeTime)).ToList();
        this.reload = reload;
        this.reloadDelegate = value => AsyncronousRecharge();
    }
    public MultiRechargingValue(ValueBounds bounds,float value,ReloadInstruction<object> reload) :
        this(bounds,value,new() { (0, -1) },reload)
    {}
    private IEnumerator AsyncronousRecharge()
    {
        RechargeStream stream = rechargeStreams.OrderBy(s=>s.currentTime).First();
        yield return reload(stream.currentTime);
        RechargeStep = 0;
        foreach(RechargeStream r in rechargeStreams)
        {
            r.currentTime -= stream.rechargeTime;
            if(r.currentTime <= 0)
            {
                r.currentTime = r.rechargeTime;
                RechargeStep += r.step;
            }
        }
        yield return null;
    }
    public class RechargeStream
    {
        public float step, rechargeTime, currentTime;

        public RechargeStream(float step,float rechargeTime,float currentTime)
        {
            this.step = step;
            this.rechargeTime = rechargeTime;
            this.currentTime = currentTime;
        }
    }
}
