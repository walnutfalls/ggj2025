using System.Collections.Generic;
using UnityEngine;

public class ResourcesInitializer : MonoBehaviour
{
    [SerializeField]
    private List<HatScriptable> hats;

    protected void Start()
    {
        this.hats.ForEach((h) => StatsTracker.Instance.RegisterHat(h));
        GameDirector.Instance.StartNewGame();
    }
}
