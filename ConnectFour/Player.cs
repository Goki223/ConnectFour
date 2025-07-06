using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
        public Player() { }
        public void AddScore()
        {
            Score++;
        }
        override public string ToString()
        {
            return $"{Name} - {Score}";
        }


    }
}
