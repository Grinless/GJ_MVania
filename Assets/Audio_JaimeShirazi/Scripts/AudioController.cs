using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AudioByJaime
{
    public enum AudioCoroutineType {
        UPGRADE_FANFAIR, 
        BOSS_START, 
        BOSS_EXIT
    }

    public class AudioController : MonoBehaviour
    {
        private static AudioController instance;

        public SoundsManager soundsManager;
        public MusicManager musicManager;
        public AmbienceManager ambienceManager;
        public EffectsManager effectsManager;

        public static AudioController Instance
        {
            get => instance;
        }

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            ambienceManager.FadeIn();
        }

        public void PlaySound(SoundEffectType soundType)
        {
            soundsManager.PlaySound(soundType);
        }

        public void EnterUpgradeRoom()
        {
            effectsManager.LowpassIn();
        }

        public void StartAudioCoroutine(AudioCoroutineType type)
        {
            switch (type)
            {
                case AudioCoroutineType.UPGRADE_FANFAIR:
                    StartCoroutine(UpgradeGet());
                    break;
                case AudioCoroutineType.BOSS_START:
                    StartCoroutine(EnterBossFight());
                    break;
                case AudioCoroutineType.BOSS_EXIT:
                    StartCoroutine(ExitBossFight());
                    break;
            }
        }

        public IEnumerator UpgradeGet()
        {
            ambienceManager.FadeOut();
            yield return new WaitForSeconds(0.8f);
            yield return new WaitForSeconds(musicManager.UpgradeMusic());
            yield return new WaitForSeconds(0.5f);
            ambienceManager.FadeIn();
            yield return new WaitForSeconds(2);
        }

        public void ExitUpgradeRoom()
        {
            effectsManager.LowpassOut();
        }

        public IEnumerator EnterBossFight()
        {
            ambienceManager.FadeOut();
            yield return new WaitForSeconds(1);
            musicManager.StartBoss();
        }

        public IEnumerator ExitBossFight()
        {
            yield return StartCoroutine(musicManager.EndBoss());
            ambienceManager.FadeIn();
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(AudioController))]
    public class AudioControllerEditor : Editor
    {
        private AudioController controller;
        private SoundEffectType soundEffectType;


        private void Awake()
        {
            controller = (AudioController)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(5);
            GUILayout.Label("~ DEBUG CONTROLS ~");
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Label("SFX Style:");
                    soundEffectType = (SoundEffectType)EditorGUILayout.EnumPopup(soundEffectType);
                }
                if (GUILayout.Button("Play"))
                {
                    controller.PlaySound(soundEffectType);
                }
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Start Boss"))
                {
                    controller.StartCoroutine(controller.EnterBossFight());
                }
                if (GUILayout.Button("End Boss"))
                {
                    controller.StartCoroutine(controller.ExitBossFight());
                }
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Ambience in"))
                {
                    controller.ambienceManager.FadeIn();
                }
                if (GUILayout.Button("Ambience out"))
                {
                    controller.ambienceManager.FadeOut();
                }
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Enter Upgrade Room"))
                {
                    controller.EnterUpgradeRoom();
                }
                if (GUILayout.Button("Exit Upgrade Room"))
                {
                    controller.ExitUpgradeRoom();
                }
            }
            if (GUILayout.Button("Upgrade Music"))
            {
                controller.StartCoroutine(controller.UpgradeGet());
            }
        }
    }
#endif
}
