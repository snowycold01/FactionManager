using Rocket.API;
using snowycold.FactionManager.Models;

namespace snowycold.FactionManager
{
    public class FactionManagerConfiguration : IRocketPluginConfiguration
    {
        public Faction[] Factions { get; set; } = new Faction[0];
        
        public void LoadDefaults()
        {
            Factions = new Faction[]
            {
                new Faction()
                {
                    FactionPermission = "FactionName",
                    OwnerSteam64ID = 76561198986609752,
                    MemberLimit  = 12
                }
            };
        }
    }
}
