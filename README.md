**Commands**
----------------------------------------
/addPlayerToFaction <playerName> - Adds the specified player from faction - /addPlayer

/removePlayerFromFaction <playerName> - Removes the specified player from faction - /removePlayer

**Config**
----------------------------------------
<FactionPermission> - This should be the Id of the permission group that faction is assigned

<OwnerSteam64ID> - This should be the Steam64ID of the owner of the faction (cannot call this command if this does not match the player calling it), if you want multiple leaders, just make a new faction with a different leader but same <FactionPermission>

<MemberLimit> - This is how many members the faction is limited to, put "0" if there should be no limit
