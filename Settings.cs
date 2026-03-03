using BTD_Mod_Helper.Api.Data;
using BTD_Mod_Helper.Api.ModOptions;

namespace Chinmod
{
    internal class Settings : ModSettings
    {
        public static readonly ModSettingBool PatchUltraboost = new(true)
        {
            description = "Gives Ultraboost ability 0 cooldown"
        };

        public static readonly ModSettingBool PatchRedeploy = new(true)
        {
            description = "Gives Redeploy ability 0 cooldown"
        };

        public static readonly ModSettingBool PatchCrush = new(true)
        {
            description = "Gives Bloon Crush an ability to kill stall"
        };
    }
}
