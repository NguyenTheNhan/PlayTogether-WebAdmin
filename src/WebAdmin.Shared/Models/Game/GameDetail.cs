using System.Collections.Generic;
using WebAdmin.Shared.Models.Rank;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Shared.Models.Game
{
    public class GameDetail : GameSummary
    {
        public List<TypeOfGameWithGameTypeDetail> typeOfGames { get; set; }
        public List<RankDetail> ranks { get; set; }
    }
}
