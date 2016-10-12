using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Domain.Entities
{
    public class Player
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
