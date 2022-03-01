using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Shared.Models.GameType
{
    public class GameTypeDetail : GameTypeSummary
    {
        List<TypeOfGameSummary> TypeOfGames { get; set; }
    }
}
