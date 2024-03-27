using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class SpeedIsDamage : Upgrade
{
    Transform followingTransform;
    float DamageMultipler, maxTime;
    struct Keypos
    {
        public float time;
        public Vector3 pos;

        public Keypos(float time,Vector3 pos)
        {
            this.time = time;
            this.pos = pos;
        }
    }
    List<Keypos> keyposes;
    void AddKeypos()
    {
        keyposes.Add(new(Time.time,followingTransform.position));
    }
    void CalculateMultipler()
    {
        keyposes = keyposes.Where(k=>(Time.time-k.time)<maxTime).ToList();
        float multipler = 0;
        for (int i = 0; i < keyposes.Count-1; i++) 
        {
            multipler += Vector3.Distance(keyposes[i].pos,keyposes[i + 1].pos);
        }
        multipler *= DamageMultipler;
    }
}