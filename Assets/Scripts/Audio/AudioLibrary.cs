using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Audio Library", menuName = "Audio/Audio Library")]
public class AudioLibrary : ScriptableObject {
    [Tooltip("List of sound objects that will hold and play audio clips with defined settings.")]
    [SerializeField] private List<Sound> _sounds;

    public List<Sound> Sounds { get => _sounds; }

    public void PlaySound(string soundName) {
        GetSound(soundName).PlaySound();
    }

    public void StopSound(string soundName) {
        GetSound(soundName).StopAudio();
    }

    private Sound GetSound(string soundName) {
        for (int i = 0; i < Sounds.Count; i++) {
            if (Sounds[i].SoundName != soundName) {
                continue;
            }

            return Sounds[i];
        }

        Debug.LogWarning($"Could not find sound with provided name {soundName}!", this);
        return null;
    }
}
