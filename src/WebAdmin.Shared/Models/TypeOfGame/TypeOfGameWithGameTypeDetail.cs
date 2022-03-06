using WebAdmin.Shared.Models.GameType;

namespace WebAdmin.Shared.Models.TypeOfGame
{
    public class TypeOfGameWithGameTypeDetail : TypeOfGameSummary
    {
        public GameTypeSummary GameType { get; set; }
    }
}
