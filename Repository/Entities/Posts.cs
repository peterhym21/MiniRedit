using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditRepository.Entities
{
    public class Posts
    {
        public int PostId { get; set;  }

        public int BoardId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public bool Deletet { get; set; }

    }
}
