using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Game;

namespace WebAdmin.Shared.Models.TypeOfGame
{
    public class TypeOfGameWithGameDetail : TypeOfGameSummary
    {
        public GameSummary Game { get; set; }
    }
}
