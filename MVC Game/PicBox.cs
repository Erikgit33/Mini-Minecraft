using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC_Game
{
    internal class PicBox : PictureBox
    {
        internal bool breakable = true;

        internal int row;
        internal int column;

        internal string type;

        public PicBox(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }
}
