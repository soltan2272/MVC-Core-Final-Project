using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
   public class PaginationParams
    {
        public const int  _maxItemsPerPage  = 20;
        public int itemsPerPage;
        public int Page { get; set; } = 1;
        public int ItemsPerPage {
            get=>itemsPerPage;
            set=>itemsPerPage=value>_maxItemsPerPage?_maxItemsPerPage:value; }
    }
}
