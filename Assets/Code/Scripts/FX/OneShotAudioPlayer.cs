using UnityEngine;

/// <summary>Component that can play a sound and then deactivate its owner
/// </summary>
public class OneShotAudioPlayer : MonoBehaviour
{
    public void PlayClip(AudioClip audioClip)
    {
        gameObject.SetActive(true);
        var clipLength = audioClip.length;
        GetComponent<AudioSource>().PlayOneShot(audioClip);
        Invoke(nameof(Disable), clipLength);
    }
    void Disable() => gameObject.SetActive(false);
}
