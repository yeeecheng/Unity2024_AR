using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_controller : MonoBehaviour
{
    // 音樂撥放器
    public AudioSource combat_bgm;


    // 撥放音樂

    public void SoundEffectPlay()
    {
        combat_bgm.Play();
    }

    public void SoundEffectVol(float vol)
    {
        combat_bgm.volume = vol;
    }
    public void SoundEffectStop()
    {
        combat_bgm.Stop();
    }
}
