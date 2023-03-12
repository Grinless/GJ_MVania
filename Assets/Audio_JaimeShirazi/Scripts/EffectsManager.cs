using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioByJaime
{
    public class EffectsManager : MonoBehaviour
    {
        public AudioMixer mixer;
        private bool targetState;
        private bool fading;
        private float cutoffBuffer;
        public void LowpassIn()
        {
            mixer.GetFloat("AmbienceLowpassCutoff", out cutoffBuffer);
            targetState = true;
            StartCoroutine(Fader());
        }
        public void LowpassOut()
        {
            mixer.GetFloat("AmbienceLowpassCutoff", out cutoffBuffer);
            targetState = false;
            StartCoroutine(Fader());
        }
        private IEnumerator Fader()
        {
            if (fading)
                yield break;
            fading = true;
            while (cutoffBuffer != (targetState ? 1500 : 22000))
            {
                cutoffBuffer = Mathf.Clamp(
                    cutoffBuffer + (targetState ? -Time.deltaTime * 15000f : Time.deltaTime * 15000f),
                    1500, 22000);
                mixer.SetFloat("AmbienceLowpassCutoff", cutoffBuffer);
                yield return null;
            }
            fading = false;
        }
    }
}
