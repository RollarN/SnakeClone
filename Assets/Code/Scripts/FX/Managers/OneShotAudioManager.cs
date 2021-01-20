using UnityEngine;

/// <summary>Static class that can objectpool and spawn oneshot audioplayers
/// </summary>
public static class OneShotAudioManager
{
    private static ObjectPool objectPool;
    public static void SetupObjectPool(int ObjectPoolCount)
    {
        var newObject = new GameObject();
        newObject.AddComponent<OneShotAudioPlayer>();
        newObject.AddComponent<AudioSource>();
        objectPool = new ObjectPool(ObjectPoolCount, newObject, new GameObject("AudioSourceParent").transform);
    }
    /// <summary>Activates an audiosource, plays the input audioclip, and then deactivates
    /// </summary>
    public static void PlayOneShot2D(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            Debug.LogWarning("PlayOneShot was executed without PlaySound");
            return;
        }
        objectPool.Rent().GetComponent<OneShotAudioPlayer>().PlayClip(audioClip);
    }
}
