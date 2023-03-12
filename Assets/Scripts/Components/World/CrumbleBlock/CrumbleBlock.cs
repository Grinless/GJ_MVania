using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CBState
{
    INACTIVE,
    FADE,
    RESET
}

public class CrumbleBlock : MonoBehaviour
{
    public CBState state = CBState.INACTIVE;

    public float _currentTime = 0;
    public BoxCollider2D collision;
    public SpriteRenderer renderer;
    public float fadeSpeed = 0.5f;
    public float timeTillReactivated = 0.5f;

    private void Update()
    {
        if (state == CBState.FADE)
        {
            if (renderer.color.a > 0)
            {
                renderer.color =
                    new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - fadeSpeed * Time.deltaTime);

                if (renderer.color.a <= 0)
                {
                    state = CBState.RESET;
                    collision.enabled = false;
                    _currentTime = timeTillReactivated;
                    //--AJ--
                    AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.Crumble);
                }
            }
        }

        if (state == CBState.RESET)
        {
            if (_currentTime >= 0)
                _currentTime -= Time.deltaTime;
            else
            {
                renderer.color =
                    new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
                collision.enabled = true;
                state = CBState.INACTIVE;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && state == CBState.INACTIVE)
            state = CBState.FADE;
    }
}
