using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioByJaime
{
    public enum SoundEffectType
    {
        Footstep, Jump, Land, Shoot, Hurt, Crumble, Talk, DoorOpen, DoorClose,
        WormMove, WormDie,
        BatSwoop, BatDie,
        BossCry, BossThrow, BossHit, BossDie
    }
    public class SoundsManager : MonoBehaviour
    {
        public List<AudioSource> footsteps, jump, jumpLand, shoot, hurt, crumble, talk, doorOpen, doorClose;
        public List<AudioSource> wormMove, wormDie;
        public List<AudioSource> batSwoop, batDie;
        public List<AudioSource> bossCry, bossThrow, bossHit, bossDie;
        public void PlaySound(SoundEffectType type)
        {
            switch (type)
            {
                case SoundEffectType.Footstep:
                    PlayRandomInRange(footsteps); break;
                case SoundEffectType.Jump:
                    PlayRandomInRange(jump); break;
                case SoundEffectType.Land:
                    PlayRandomInRange(jumpLand); break;
                case SoundEffectType.Shoot:
                    PlayRandomInRange(shoot); break;
                case SoundEffectType.Hurt:
                    PlayRandomInRange(hurt); break;
                case SoundEffectType.Crumble:
                    PlayRandomInRange(crumble); break;
                case SoundEffectType.Talk:
                    PlayRandomInRange(talk); break;
                case SoundEffectType.DoorOpen:
                    PlayRandomInRange(doorOpen); break;
                case SoundEffectType.DoorClose:
                    PlayRandomInRange(doorClose); break;

                case SoundEffectType.WormMove:
                    PlayRandomInRange(wormMove); break;
                case SoundEffectType.WormDie:
                    PlayRandomInRange(wormDie); break;

                case SoundEffectType.BatSwoop:
                    PlayRandomInRange(batSwoop); break;
                case SoundEffectType.BatDie:
                    PlayRandomInRange(batDie); break;

                case SoundEffectType.BossCry:
                    PlayRandomInRange(bossCry); break;
                case SoundEffectType.BossHit:
                    PlayRandomInRange(bossHit); break;
                case SoundEffectType.BossThrow:
                    PlayRandomInRange(bossThrow); break;
                case SoundEffectType.BossDie:
                    PlayRandomInRange(bossDie); break;
            }
        }
        public void PlayRandomInRange(List<AudioSource> range, float pitchBase = 1, float pitchVariance = 0.01f)
        {
            int randomIndex = Random.Range(0, range.Count);
            if (range.Count > 1)
            {
                while (range[randomIndex].isPlaying) //if that footstep sound was already playing, choose a different one
                    randomIndex = Random.Range(0, range.Count);
            }
            range[randomIndex].pitch = Random.Range(pitchBase - pitchVariance, pitchBase + pitchVariance);
            range[randomIndex].Play();
        }
    }
}