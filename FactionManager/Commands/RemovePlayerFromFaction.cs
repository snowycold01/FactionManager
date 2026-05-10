using System.Collections.Generic;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Steam;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.SteamworksProvider;
using SDG.Unturned;
using snowycold.FactionManager.Models;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;


namespace snowycold.FactionManager.Commands;

public class RemovePlayerFromFaction : IRocketCommand
{
    public void Execute(IRocketPlayer caller, string[] command)
    {
        UnturnedPlayer player = caller as UnturnedPlayer;
        if (player == null) return;

        if (command == null || command.Length < 1)
        {
            UnturnedChat.Say(player, FactionManagerPlugin.Instance.Translate("SyntaxInvalid", Name, Syntax), Color.red);
            return;
        }

        Faction ownedFaction = null;
        Faction[] factions = FactionManagerPlugin.Instance.Configuration.Instance.Factions ?? new Faction[0];
        foreach (Faction faction in factions)
        {
            if (faction != null && faction.OwnerSteam64ID.ToString().Equals(player.CSteamID.ToString()))
            {
                ownedFaction = faction;
                break;
            }
        }

        if (ownedFaction == null)
        {
            UnturnedChat.Say(player, FactionManagerPlugin.Instance.Translate("PermissionInvalid"), Color.red);
            return;
        }

        string groupId = ownedFaction.FactionPermission;
        if (R.Permissions.GetGroup(groupId) == null)
        {
            UnturnedChat.Say(player, FactionManagerPlugin.Instance.Translate("GroupInvalid"), Color.red);
            Logger.Log($"{groupId} does not exist");
            return;
        }

        UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
        if (target == null)
        {
            UnturnedChat.Say(player, FactionManagerPlugin.Instance.Translate("PlayerInvalid", command[0]), Color.red);
            return;
        }

        R.Permissions.RemovePlayerFromGroup(groupId, target);
        UnturnedChat.Say(player, FactionManagerPlugin.Instance.Translate("RemovePlayer", target.DisplayName), Color.green);
    }

    public AllowedCaller AllowedCaller => AllowedCaller.Player;
    public string Name => "removePlayerFromFaction";
    public string Help => "Removes a player to the faction";
    public string Syntax => "<playerName>";
    public List<string> Aliases { get; } = new List<string>() {"removeplayer"};
    public List<string> Permissions { get; } = new List<string> {"FactionManager.RemovePlayer"};
}
