using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.GameType;

namespace WebAdmin.Shared.Models.TypeOfGame
{
    public class TypeOfGameWithGameTypeDetail : TypeOfGameSummary
    {
        public GameTypeSummary GameType { get; set; }
    }
}
