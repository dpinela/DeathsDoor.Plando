using Bep = BepInEx;
using HL = HarmonyLib;
using IC = DDoor.ItemChanger;
using AGM = DDoor.AlternativeGameModes;

namespace DDoor.Plando;

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
