using Bep = BepInEx;
using HL = HarmonyLib;
using IC = DDoor.ItemChanger;
using AGM = DDoor.AlternativeGameModes;
using Collections = System.Collections.Generic;
using static System.Linq.Enumerable;
using IO = System.IO;

namespace DDoor.Plando;

[Bep.BepInPlugin("deathsdoor.plando", "Plando", "1.1.0.0")]
[Bep.BepInDependency("deathsdoor.alternativegamemodes", "1.0")]
[Bep.BepInDependency("deathsdoor.itemchanger", "1.2")]
internal class PlandoPlugin : Bep.BaseUnityPlugin
{
    public void Start()
    {
        var plandoFiles = InstalledPlandoFiles();
        var plandos = plandoFiles
            .Select(f => new PlandoFile {
                FileName = f,
                Name = IO.Path.GetFileNameWithoutExtension(f)
            })
            .OrderBy(p => p.Name, System.StringComparer.InvariantCultureIgnoreCase)
            .ToList();
        foreach (var plando in plandos)
        {
            Logger.LogInfo($"Found: [{plando.Name}]");
            var modeName = "START " + plando.Name.ToUpper();
            AGM.AlternativeGameModes.Add(modeName, () =>
            {
                try
                {
                    var p = Plando.Read(plando.FileName, Logger.LogWarning);
                    var data = IC.SaveData.Open();
                    foreach (var pl in p.Placements)
                    {
                        data.Place(item: pl.ItemName, location: pl.LocationName);
                    }
                    if (p.StartNight)
                    {
                        GameSave.currentSave.SetNightState(true);
                    }
                    if (p.StartWeapon != "sword")
                    {
                        data.StartingWeapon = p.StartWeapon;
                        // Actually equip the chosen weapon.
                        // (IC does not do this for you)
                        GameSave.currentSave.weaponId = p.StartWeapon;
                    }
                }
                catch (System.Exception err)
                {
                    Logger.LogError($"Error loading [{plando.Name}]: {err}");
                }
            });
        }
    }

    private static string[] InstalledPlandoFiles()
    {
        var bepInExDir = IO.Path.GetDirectoryName(
            IO.Path.GetDirectoryName(
                typeof(Bep.BaseUnityPlugin).Assembly.Location));
        var pluginDir = IO.Path.Combine(bepInExDir, "plugins");
        return IO.Directory.GetFiles(pluginDir, "*.ddplando", IO.SearchOption.AllDirectories);
    }

    private struct PlandoFile
    {
        internal string Name;
        internal string FileName;
    }
}
