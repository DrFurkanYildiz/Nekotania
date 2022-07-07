using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    public ParticleSystem SnowParticle;

    private void Awake()
    {
        Instance = this;
        SnowParticle.Stop();
    }
}
