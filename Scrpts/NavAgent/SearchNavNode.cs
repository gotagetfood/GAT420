using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrpts.Agent
{
    internal class SearchNavNode : NavNode
    {
        public NavNode parent { get; set; } = null;
        public bool visited { get; set; } = false;
    }
}
