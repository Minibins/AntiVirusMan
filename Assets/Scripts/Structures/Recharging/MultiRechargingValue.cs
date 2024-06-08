using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MultiRechargingValue : RechargingValue
{
    private List<RechargeStream> rechargeStreams;
    private ReloadInstruction<object> reload;
    public MultiRechargingValue(ValueBounds bounds,List<(float step, float rechargeTime)> rechargeStreams,float rechargeStep,ReloadInstruction<object> reload) : base(bounds,rechargeStreams[0].step,rechargeStreams[0].rechargeTime,rechargeStep,null)
    {
        this.rechargeStreams = rechargeStreams.Select(val=>new RechargeStream(val.step,val.rechargeTime,val.rechargeTime)).ToList();
        this.reload = reload;
        this.reloadDelegate = value => AsyncronousRecharge();
    }
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
    private class RechargeStream
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
