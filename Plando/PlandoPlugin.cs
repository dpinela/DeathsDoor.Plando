using Bep = BepInEx;
using HL = HarmonyLib;
using IC = DeathsDoor.ItemChanger;
using AGM = DeathsDoor.AlternativeGameModes;

namespace DeathsDoor.Plando;

[Bep.BepInPlugin("deathsdoor.plando", "Plando", "1.0.0.0")]
internal class PlandoPlugin : Bep.BaseUnityPlugin
{
    public void Start()
    {
        AGM.AlternativeGameModes.Add("START PLANDO", () =>
        {
            var data = IC.SaveData.Open();
            data.Placements.Add(new() { ItemName = "Hookshot", LocationName = "Grove of Spirits Door" });
        });
    }
}
