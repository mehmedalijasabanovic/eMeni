using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.Business.Commands.UpdatePromotionRank
{
    public sealed class UpdateBusinessPromotionRankCommand:IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public byte PromotionRank { get; set; }
    }
}
