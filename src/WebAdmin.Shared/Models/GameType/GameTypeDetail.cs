using System.Collections.Generic;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Shared.Models.GameType
{
    public class GameTypeDetail : GameTypeSummary
    {
        List<TypeOfGameSummary> TypeOfGames { get; set; }
    }
}
