using Collections = System.Collections.Generic;
using IO = System.IO;
using IC = DDoor.ItemChanger;

namespace DDoor.Plando;

internal class Plando
{
    public Collections.List<Placement> Placements = new();
    public bool StartNight = false;
    public string StartWeapon = "sword";

    public static Plando Read(string filename, System.Action<string> logError)
    {
        using var file = IO.File.OpenText(filename);
        var plando = new Plando();
        var lineNum = 0;
        while (true)
        {
            lineNum++;
            var line = file.ReadLine();
            if (line == null)
            {
                return plando;
            }
            if (line.StartsWith("#"))
            {
                var opt = line.Substring(1).Trim();
                if (!plando.ApplySpecialOption(opt))
                {
                    logError($"syntax error on {filename}:{lineNum}: unknown option {opt}");
                }
                continue;
            }
            var at = line.IndexOf('@');
            if (at == -1)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    logError($"syntax error on {filename}:{lineNum}: missing @");
                }
                continue;
            }
            plando.Placements.Add(new Placement {
                ItemName = line.Substring(0, at).Trim(),
                LocationName = line.Substring(at + 1).Trim()
            });
        }
    }

    private bool ApplySpecialOption(string opt)
    {
        switch (opt)
        {
            case "start-night":
                StartNight = true;
                return true;
            case "start-weapon-daggers":
                StartWeapon = "daggers";
                return true;
            case "start-weapon-umbrella":
                StartWeapon = "umbrella";
                return true;
            case "start-weapon-greatsword":
                StartWeapon = "sword_heavy";
                return true;
            case "start-weapon-hammer":
                StartWeapon = "hammer";
                return true;
            default:
                return false;
        }
    }
}

internal class Placement
{
    public string LocationName = "";
    public string ItemName = "";
}
