using BepInEx;
using HarmonyLib;
using Steamworks;
using Steamworks.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuckAchievements
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("Muck.exe")]
    public class Main : BaseUnityPlugin
    {
        string achievementName = "";
        string achievementCount = "";
        int achievementCountInt = 1;

        public const string
            GUID = "Frogger.MuckAchievements",
            NAME = "MuckAchievements",
            VERSION = "0.0.1";

        public void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(MuckPatch));
            Logger.LogMessage("Loaded MuckAchievements");
        }

        public void OnGUI()
        {
            GUILayout.BeginArea(new Rect(20, 10 ,300, 300));

            achievementName = GUILayout.TextField(achievementName, 25);
            achievementCount = GUILayout.TextField(achievementCount, 3);
            
            if (GUILayout.Button("Add achievement")) AddAchievement();
            if (GUILayout.Button("Set achievement")) SetAchievement();

            GUILayout.EndArea();
        }

        private void Update()
        {
            if (achievementCount != string.Empty) Int32.TryParse(achievementCount, out achievementCountInt);
            else achievementCountInt = 1;
        }

        private void AddAchievement()
        {
            SteamUserStats.AddStat(achievementName, achievementCountInt);
            Logger.LogMessage($"Add {achievementCountInt} to {achievementName} stat");
        }
        private void SetAchievement()
        {
            SteamUserStats.SetStat(achievementName, achievementCountInt);
            Logger.LogMessage($"Set Stat {achievementName} to {achievementCountInt}");
        }
    }
}

public class MuckPatch { }
