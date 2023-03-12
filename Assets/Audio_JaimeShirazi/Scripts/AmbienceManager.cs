using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioByJaime
{
    public class AmbienceManager : MonoBehaviour
    {
        public float maxVolume;
        public AudioSource ambience;
        private bool targetState;
        private bool fading;
        public void FadeIn()
        {
            targetState = true;
            StartCoroutine(Fader());
        }
        public void FadeOut()
        {
            targetState = false;
            StartCoroutine(Fader());
        }
        private IEnumerator Fader()
        {
            if (fading)
                yield break;
            fading = true;
            while (ambience.volume != (targetState ? maxVolume : 0))
            {
                ambience.volume = Mathf.Clamp(
                    ambience.volume + (targetState ? Time.deltaTime / 4f : -Time.deltaTime / 4f),
                    0, maxVolume);
                yield return null;
            }
            fading = false;
        }
    }
}