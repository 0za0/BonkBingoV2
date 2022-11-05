using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoOnline.Models
{
    public class BingoButtonModel
    {
        public int Number { get; private set; }
        public bool IsPressed { get; set; }
        public string Text { get; set; }
        public BingoButtonModel(int n)
        {
            Number = n;
            Text = $"Button {Number}";
        }


        public void ClickButton()
        {
            IsPressed = !IsPressed;
            Text = IsPressed ? "Pressed" : "Not Pressed";
        }
    }
}
