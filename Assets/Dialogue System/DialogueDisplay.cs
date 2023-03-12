using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using AudioByJaime;
using UnityEditor;

public class DialogueDisplay : MonoBehaviour
{
    public float charInterval = 0.025f;
    public int soundEveryXChars = 3;
    public TextMeshProUGUI text;
    public Image backBox;
    private string target;
    public void ShowDialogue(string dialogue, bool isUpgrade)
    {
        if (target != dialogue)
        {
            target = dialogue;
            text.text = "";
            if (isUpgrade)
            {
                text.alignment = isUpgrade ? TextAlignmentOptions.Center : TextAlignmentOptions.Left;
            }
            backBox.enabled = true;
            StartCoroutine(AnimateText(isUpgrade));
        }
    }
    IEnumerator AnimateText(bool isUpgrade)
    {
        string targetBuffer = target;
        int charCounter = 0;
        foreach (char c in target)
        {
            if (targetBuffer != target)
                yield break;
            text.text += c;
            if (!isUpgrade)
            {
                charCounter++;
                if (soundEveryXChars <= charCounter)
                {
                    AudioController.Instance.PlaySound(SoundEffectType.Talk);
                    charCounter = 0;
                }
            }
            yield return new WaitForSeconds(charInterval);
        }
    }
    public void HideDialogue()
    {
        StopAllCoroutines();
        text.text = "";
        backBox.enabled = false;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(DialogueDisplay))]
public class DialogueDisplayEditor : Editor
{
    private DialogueDisplay display;
    private string debugText;
    private bool isUpgrade;
    private void Awake()
    {
        display = (DialogueDisplay)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(5);
        GUILayout.Label("~ DEBUG CONTROLS ~");
        isUpgrade = EditorGUILayout.Toggle("Is upgrade? (skip sound)", isUpgrade);
        using (new EditorGUILayout.HorizontalScope())
        {
            debugText = EditorGUILayout.TextField("Debug Text", debugText);
            if (GUILayout.Button("Show"))
            {
                display.ShowDialogue(debugText, isUpgrade);
            }
        }
        if (GUILayout.Button("Close"))
        {
            display.HideDialogue();
        }
    }
}
#endif
