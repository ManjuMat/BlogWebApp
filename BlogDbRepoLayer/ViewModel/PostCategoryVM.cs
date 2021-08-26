using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDbRepoLayer.ViewModel
{
    public class PostCategoryVM
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CategoryName { get; set; }
    }
}
