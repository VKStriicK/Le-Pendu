using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le_Pendu
{
    public class Mots
    {
        private string _Mots;
        public string mots => _Mots;

        private int _TailleDuMot;
        public int tailleDuMot => _TailleDuMot;

        public Mots(string val) //Constructeur
        {
            _Mots = val;
            _TailleDuMot = val.Length;
        }
    }
}
