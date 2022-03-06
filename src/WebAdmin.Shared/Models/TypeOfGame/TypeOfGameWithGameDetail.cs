using WebAdmin.Shared.Models.Game;

namespace WebAdmin.Shared.Models.TypeOfGame
{
    public class TypeOfGameWithGameDetail : TypeOfGameSummary
    {
        public GameSummary Game { get; set; }
    }
}
