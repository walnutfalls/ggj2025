using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : SingletonBase<StatsTracker>
{
    public enum HatStatus { Locked, UnlockedInPrevious, UnlockedInCurrent };

    public int GamesStarted { get; private set; } = 0;

    private readonly Dictionary<string, HatStatus> hatUnlockStatuses = new();

    private readonly List<HatScriptable> hatsRegistered = new();

    public void GetHatStatuses(List<Tuple<HatStatus, HatScriptable>> statuses)
    {
        statuses.Clear();

        foreach (var hat in this.hatsRegistered)
        {
            var hatUnlockStatus = this.hatUnlockStatuses.ContainsKey(hat.name)
                ? this.hatUnlockStatuses[hat.name]
                : HatStatus.Locked;

            statuses.Add(new(hatUnlockStatus, hat));
        }
    }

    public void OnGameStarted()
    {
        this.GamesStarted++;

        foreach (var kvp in this.hatUnlockStatuses)
        {
            if (kvp.Value == HatStatus.UnlockedInCurrent)
            {
                this.hatUnlockStatuses[kvp.Key] = HatStatus.UnlockedInPrevious;
            }
        }
    }

    public void OnHatCollected(HatScriptable hat)
    {
        hatUnlockStatuses[hat.name] = HatStatus.UnlockedInCurrent;
    }

    public void RegisterHat(HatScriptable hat)
    {
        for (var i = 0; i < this.hatsRegistered.Count; i++)
        {
            if (this.hatsRegistered[i].name == hat.name)
            {
                this.hatsRegistered[i] = hat;
                return;
            }
        }

        this.hatsRegistered.Add(hat);
        this.hatsRegistered.Sort((a, b) => {
            var valueDifference = Mathf.RoundToInt(a.HatValue) - Mathf.RoundToInt(b.HatValue);
            if (valueDifference != 0)
            {
                return valueDifference;
            }

            return a.Name.CompareTo(b.Name);
        });
    }
}
