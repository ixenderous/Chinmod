global using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper;
using Chinmod;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using MelonLoader;
using UnityEngine.InputSystem.Utilities;

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

        if (Settings.PatchUltraboost) patchUltraboost(result);
        if (Settings.PatchRedeploy) patchRedeploy(result);
        if (Settings.PatchCrush) patchCrush(result);
    }

    private void patchUltraboost(GameModel result)
    {
        var engis = result.GetTowersWithBaseId("EngineerMonkey").ToList();
        foreach (var engi in engis)
        {
            if (engi.tiers[1] != 5) continue;

            var ability = engi.GetAbility();
            ability.maxActivationsPerRound = 11;
            ability.cooldown = 0;
            ability.Cooldown = 0;
        }
    }

    private void patchRedeploy(GameModel result)
    {
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

    private void patchCrush(GameModel result)
    {
        var bombs = result.GetTowersWithBaseId("BombShooter").ToList();

        var plane = result.GetTower("MonkeyAce", 0, 5, 0);
        if (plane == null) return;

        var nuke = plane.GetAbility();
        if (nuke == null) return;

        nuke = nuke.Clone().As<AbilityModel>();
        nuke.RemoveBehaviors<AbilityBehaviorModel>();
        nuke.AddBehavior(new AbilityDamageAllModel("AbilityDamageAllModel", 9999999, new string[] { "Bad" }, false));
        nuke.cooldown = 0;
        nuke.Cooldown = 0;

        foreach (var bomb in bombs)
        {
            if (bomb.tiers[0] < 5) continue;

            bomb.AddBehavior(nuke);
        }
    }
}