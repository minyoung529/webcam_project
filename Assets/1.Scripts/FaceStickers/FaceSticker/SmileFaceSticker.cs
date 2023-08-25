using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileFaceSticker : FaceSticker
{
    [SerializeField]
    private ParticleSystem[] particle;

    public override void ChildInit()
    {
        base.ChildInit();
        faceController.Event.StartListening((int)FaceEvent.EmotionChanged, OnChanged);
    }

    public override void ChildDestroyed()
    {
        base.ChildDestroyed();
        faceController.Event.StopListening((int)FaceEvent.EmotionChanged, OnChanged);
    }

    private void OnChanged(object emotion)
    {
        string sEmotion = emotion.ToString();

        if (sEmotion == Emotion.Happy.ToString())
        {
            for (int i = 0; i < particle.Length; i++)
            {
                particle[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < particle.Length; i++)
            {
                particle[i].Stop();
            }
        }
    }
}
