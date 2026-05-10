using System.Runtime.Remoting.Messaging;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using snowycold.FactionManager.Models;
using Steamworks;

namespace snowycold.FactionManager
{
    public class FactionManagerPlugin : RocketPlugin<FactionManagerConfiguration>
    {
        public static FactionManagerPlugin Instance { get; private set; }
        
        protected override void Load()
        {
            Instance = this;
            Logger.Log("\n-=-=-=-Faction Manager v1.0.0-=-=-=-\n-=-By: snowycold-=-=-\n-=-=-=-Has Been Loaded-=-=-=-");
            U.Events.OnPlayerConnected += OnPlayerConnected;
        }

        protected override void Unload()
        {
            Logger.Log("\n-=-=-=-Faction Manager v1.0.0-=-=-=-\n-=-By: snowycold-=-=-\n-=-=-=-Has Been Unloaded-=-=-=-");
            U.Events.OnPlayerConnected -= OnPlayerConnected;
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            if (Configuration.Instance.Factions == null) return;

            string steam64String = player.CSteamID.m_SteamID.ToString();

            foreach (Faction faction in Configuration.Instance.Factions)
            {
                if (faction == null) continue;
                if (faction.OwnerSteam64ID.ToString() != steam64String) continue;

                RocketPermissionsGroup group = R.Permissions.GetGroup(faction.FactionPermission);
                if (group == null)
                {
                    Logger.Log($"\"{faction.FactionPermission}\" does not exist!");
                    continue;
                }

                if (!group.Members.Contains(steam64String))
                {
                    R.Permissions.AddPlayerToGroup(faction.FactionPermission, player);
                }
            }
        }
        
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "PermissionInvalid", "You do not have permission to use this command!" },
            { "SyntaxInvalid", "Usage: /{0} {1}" },
            { "PlayerInvalid", "Could not find {0}"},
            { "GroupInvalid", "No permission group exists! Please contact staff."},
            { "AddPlayer", "Successfully added {0} to your faction!"},
            { "RemovePlayer", "Successfully removed {0} from your faction!"},
            { "MemberLimit", "Your group member limit has been reached!"}
        };
    }
}
