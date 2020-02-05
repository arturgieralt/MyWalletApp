using System.Collections.Generic;

namespace MyWalletApp.DomainModel.Models
{
    public class Tag: BaseModel
    {
        public string Name {get; set;}
        public ICollection<TransactionTag> TransactionTags {get; set;}
    }
}