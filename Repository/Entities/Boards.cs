using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditRepository.Entities
{
    public class Boards
    {
        public int BoardId { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public bool Deletet { get; set; }

    }
}
