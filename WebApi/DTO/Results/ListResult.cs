
using System.Collections.Generic;

namespace MyWalletApp.WebApi.DTO.Results
{
    public class ListResult<T>
    {
        public List<T> Items {get; set;}        
    }
}