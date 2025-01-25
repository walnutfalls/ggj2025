using UnityEngine;

[DisallowMultipleComponent]
public class AudioSystem : MonoBehaviour {
    [Tooltip("Audio library scriptable to pull sounds from.")]
    [SerializeField] private AudioLibrary _audioLibrary;

    private AudioSystem _instance;
    public AudioSystem Instance { get => _instance; set { _instance = value; } }


    private void Awake() {
        if (Instance != null) {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        GenerateAudioSources();
    }

    private void Start() {
        PlaySound("Background Music");
    }

    public void PlaySound(string soundName) {
        _audioLibrary.PlaySound(soundName);
    }

    private void GenerateAudioSources() {
        for (int i = 0; i < _audioLibrary.Sounds.Count; i++) {
            AudioSource _newSource = new GameObject($"Sound_{_audioLibrary.Sounds[i].SoundName}").AddComponent<AudioSource>();
            _newSource.transform.SetParent(transform);
            _audioLibrary.Sounds[i].InitSound(_newSource);
        }
    }
}
