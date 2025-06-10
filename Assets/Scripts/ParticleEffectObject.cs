using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleEffectObject : MonoBehaviour
{
    public List<ParticleSystem> effects;

    public void Play() => StartEffects();
    
    private void OnEnable() => StopEffects();
    private void OnDisable() => StopEffects();

    private void StopEffects()
    {
        foreach (ParticleSystem effect in effects)
        {
            effect.Stop();
        }
    }

    private void StartEffects()
    {
        foreach (ParticleSystem effect in effects)
        {
            effect.Play();
        }
    }

    public bool IsPlaying()
    {
        var result = false;
        foreach (var effect in effects.Where(effect => effect.isPlaying))
        {
            result = true;
        }
        
        return result;
    }
}