global using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BTD_Mod_Helper;
using Chinmod;
using Il2CppAssets.Scripts.Models;

[assembly: MelonInfo(typeof(Chinmod.Chinmod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Chinmod;

public class Chinmod : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<Chinmod>("Chinmod loaded!");
    }

    public override void OnNewGameModel(GameModel result)
    {
        base.OnNewGameModel(result);

        var engis = result.GetTowersWithBaseId("EngineerMonkey").ToList();
        foreach (var engi in engis)
        {
            if (engi.tiers[1] != 5) continue;

            var ability = engi.GetAbility();
            ability.maxActivationsPerRound = 11;
            ability.cooldown = 0;
            ability.Cooldown = 0;
        }

        var helis = result.GetTowersWithBaseId("HeliPilot").ToList();
        foreach (var heli in helis)
        {
            if (heli.tiers[1] < 4) continue;

            var ability = heli.GetAbilities().Find(a => a.name == "AbilityModel_Redeploy");
            if (ability != null)
            {
                ability.cooldown = 0;
                ability.Cooldown = 0;
            }
        }
    }
}