using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC_Game
{
    internal class Character : PictureBox
    {
        internal PictureBox detector = new PictureBox();   
        Image character;

        public Character(Image character, int picSize) 
        {
            this.character = character;

            Height = picSize * 2;
            Width = picSize;

            detector.Height = picSize;
            detector.Width = picSize;

            Image = character;
            ForeColor = Color.Transparent;
            SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
