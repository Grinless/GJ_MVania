using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioByJaime
{
    public class MusicManager : MonoBehaviour
    {
        public AudioSource itemGet, bossIn, bossLoop, bossOut;
        private double bossStartOffset;
        /// <returns>The length of the item get clip</returns>
        public float UpgradeMusic()
        {
            itemGet.Play();
            return itemGet.clip.length;
        }
        /// <param name="secondsFromCall">The amount of seconds after the method is called to start the alarm sound. The higher the better.</param>
        public void StartBoss()
        {
            bossIn.PlayScheduled(AudioSettings.dspTime);
            bossLoop.PlayScheduled(AudioSettings.dspTime + 17); //hardcoding bossIn length for precision
            bossStartOffset = AudioSettings.dspTime + 17;
        }
        public IEnumerator EndBoss()
        {
            double bossEndOffset = bossStartOffset;
            while (bossEndOffset < AudioSettings.dspTime) bossEndOffset += 1.5;
            bossOut.PlayScheduled(bossEndOffset);
            yield return new WaitUntil(() => bossEndOffset <= AudioSettings.dspTime);
            bossIn.Stop(); bossLoop.Stop();
            yield return new WaitForSeconds(bossOut.clip.length);
        }
    }

}