using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Rank;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Shared.Models.Game
{
    public class GameDetail : GameSummary
    {
        List<TypeOfGameDetail> typeOfGames { get; set; }
        List<RankDetail> ranks { get; set; }
    }
}
