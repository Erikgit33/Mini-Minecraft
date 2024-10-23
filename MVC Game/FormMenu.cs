using MVC_Game.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC_Game
{
    public partial class FormMenu : Form
    {
        public int boxesHorizontally;
        public int boxesVertically;
        public int picSize;

        private View view;
        private Model model;
        private Controller controller;

        public FormMenu()
        {
            InitializeComponent();

            this.BackgroundImage = Resources.MenuBackground;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                boxesHorizontally = Convert.ToInt32(textBoxHorizontally.Text);
                boxesVertically = Convert.ToInt32(textBoxVertically.Text);
                picSize = Convert.ToInt32(textBoxPicSize.Text);

                if (boxesHorizontally > 50) { MessageBox.Show("The number of boxes horizontally cannot exceed 50."); }
                else if (boxesVertically > 50) { MessageBox.Show("The number of boxes vertically vannot exceed 50."); }
                else if (picSize > 50) { MessageBox.Show("The block size cannot exceed 50."); }
                else if (boxesHorizontally == 0) { MessageBox.Show("The nubmer of boxes horizontally must be greater than 0."); }
                else if (boxesVertically < 15) { MessageBox.Show("The number of boxes vertically must be greater than or equal to 15."); }
                else if (picSize == 0) { MessageBox.Show("The block size must be greater than 0."); } 
                else
                {
                    view = new View(boxesVertically, boxesHorizontally, picSize, this);
                    model = new Model(boxesVertically, boxesHorizontally);
                    controller = new Controller(model, view);

                    this.Hide();
                    view.Show();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Input was incorrect. Please check it an try again!");
                MessageBox.Show(E.ToString());
            }
        }
    }
}
