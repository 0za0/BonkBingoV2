using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoOnline.Models
{
    public class BingoButtonModel
    {
        public int Number { get; private set; }
        public string Text { get; private set; }
        public BingoButtonModel(int n)
        {
            Number = n;
            Text = $"Button - {Number}";
        }
    }
}
