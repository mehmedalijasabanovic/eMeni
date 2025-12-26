using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.Menu.Commands.UpdatePromotionRank
{
    public sealed class UpdateMenuPromotionRankCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public byte PromotionRank { get; set; }
    }
}
