namespace snowycold.FactionManager.Models;

public class Faction
{
    public string FactionPermission { get; set; }
    public ulong OwnerSteam64ID { get; set; }
    public int MemberLimit { get; set; }
}