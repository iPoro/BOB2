using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOB2
{
    
    class Poäng : IComparable<Poäng>
    {
        private String namn;
        private int poäng;

        public Poäng(String _namn, int _poäng)
        {
            namn = _namn;
            poäng = _poäng;
        }
        //Kollar vilket objekt som har flest poäng
        public int CompareTo(Poäng obj)
        {
            return obj.getPoäng()-poäng;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Namn på poänginnehavaren</returns>
        public String getNamn()
        {
            return namn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Antal poäng</returns>
        public int getPoäng()
        {
            return poäng;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Poängen i utskriftsvänligt format</returns>
        public override string ToString()
        {
            return namn + "&" + poäng;
        }
    }
}
