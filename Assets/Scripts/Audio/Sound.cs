using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Audio/Sound")]
[System.Serializable]
public class Sound : ScriptableObject {
    [Tooltip("Name of this sound.")]
    [SerializeField] private string _soundName = "New Sound";
    public string SoundName { get => _soundName; }

    [Tooltip("Audio clip this sound should play.")]
    [SerializeField] private AudioClip _clip;

    [Tooltip("Volume this clip should play at.")]
    [SerializeField, Range(0f, 2f)] private float _volume = 1f;

    [Tooltip("Variability in volume as a percentage range (0.10 means a volume of 1 can vary from 0.95 to 1.05")]
    [SerializeField, Range(0, 1f)] private float _volumeRange = 0.1f;

    [Tooltip("Whether this clip should loop.")]
    [SerializeField] private bool _loop = false;

    [Tooltip("What pitch this clip should play at.")]
    [SerializeField, Range(0f, 2f)] private float _pitch = 1f;

    [Tooltip("Variability in pitch as a percentage range (0.10 means a pitch of 1 can vary from 0.95 to 1.05")]
    [SerializeField, Range(0, 1f)] private float _pitchRange = 0.1f;

    private AudioSource _source;

    public void InitSound(AudioSource source) {
        _source = source;
        _source.clip = _clip;
    }

    public void PlaySound() {
        _source.pitch = _pitch + (Random.Range(-_pitchRange * 0.5f, _pitchRange * 0.5f));
        _source.volume = _volume + (Random.Range(-_volumeRange * 0.5f, _volumeRange * 0.5f));
        _source.loop = _loop;
        _source.Play();
    }

    public void StopAudio() {
        _source.Stop();
    }
}
