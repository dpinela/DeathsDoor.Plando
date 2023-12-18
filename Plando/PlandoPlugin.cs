using Bep = BepInEx;
using HL = HarmonyLib;
using IC = DDoor.ItemChanger;
using AGM = DDoor.AlternativeGameModes;
using Collections = System.Collections.Generic;
using static System.Linq.Enumerable;
using IO = System.IO;

namespace DDoor.Plando;

[Bep.BepInPlugin("deathsdoor.plando", "Plando", "1.0.0.0")]
internal class PlandoPlugin : Bep.BaseUnityPlugin
{
    public void Start()
    {
        var plandoFiles = InstalledPlandoFiles();
        var plandos = plandoFiles
            .Select(f => new Plando {
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
                    var placements = ReadPlando(plando.FileName);
                    var data = IC.SaveData.Open();
                    data.Placements.AddRange(placements);
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

    private struct Plando
    {
        internal string Name;
        internal string FileName;
    }

    private Collections.List<IC.Placement> ReadPlando(string filename)
    {
        using var file = IO.File.OpenText(filename);
        var rows = new Collections.List<IC.Placement>();
        var lineNum = 0;
        while (true)
        {
            lineNum++;
            var line = file.ReadLine();
            if (line == null)
            {
                return rows;
            }
            var at = line.IndexOf('@');
            if (at == -1)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Logger.LogWarning($"syntax error on {filename}:{lineNum}: missing @");
                }
                continue;
            }
            rows.Add(new IC.Placement {
                ItemName = line.Substring(0, at).Trim(),
                LocationName = line.Substring(at + 1).Trim()
            });
        }
    }
}
