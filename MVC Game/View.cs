using MVC_Game.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MVC_Game
{
    public partial class View : Form
    {
        private int boxesHorizontally = 0;
        private int boxesVertically = 0;
        private int picSize = 0; 

        private Label labelScore = new Label();
        private Controller controller;

        private Character character;
        private string[,] types;
        private Form menu;

        // The time it takes for the character to 
        // fall down after jumping, in milliseconds
        private const int jumpSpeed = 450;

        public View(int boxesVertically, int boxesHorizontally, int picSize, Form menu)
        {
            InitializeComponent();

            this.boxesHorizontally = boxesHorizontally;
            this.boxesVertically = boxesVertically;
            this.picSize = picSize;
            this.menu = menu;

            int clientSizeX = picSize * boxesHorizontally;
            int clientSizeY = picSize * boxesVertically;

            SetClientSizeCore(clientSizeX, clientSizeY);

            // Same color as "Sky"
            this.BackColor = Color.FromArgb(153, 217, 234);

            timerJump.Interval = jumpSpeed;
        }

        internal void MakeGridAndSetControl(Controller controller)
        {
            this.controller = controller;

            controller.MakeGrid();
        }

        internal void SetGridToView(PicBox[,] picBoxes)
        {
            types = new string[boxesVertically, boxesHorizontally];

            Random rng = new Random();

            int columnsAdded = 0;
            int boxesAdded = 0;

            for (int indexColumn = 0; indexColumn < boxesHorizontally; indexColumn++)
            {
                for (int indexRow = 0; indexRow < boxesVertically; indexRow++)
                {
                    PicBox p = picBoxes[indexRow, indexColumn];

                    p.Width = picSize;
                    p.Height = picSize;

                    p.Left = 0 + columnsAdded * picSize;
                    p.Top = 0 + boxesAdded * picSize;

                    p.SizeMode = PictureBoxSizeMode.StretchImage;

                    p.row = indexRow;
                    p.column = indexColumn;

                    Controls.Add(p);


                    // Bedrock (boxesVertically)
                    // Prioritizes bedrock and lava to always be
                    // placed at the bottom of the map
                    if (p.row == boxesVertically - 1) 
                    {
                        p.breakable = false;
                        p.Image = Resources.Bedrock;
                        p.type = "Bedrock";
                    }
                    // Bedrock 25%/Stone 25%/Lava 50%
                    else if (p.row == boxesVertically - 2)
                    {
                        int random = rng.Next(0, 4);
                        if (random == 0)
                        {
                            p.breakable = false;
                            p.Image = Resources.Bedrock;
                            p.type = "Bedrock";
                        }
                        else if (random == 1)
                        {
                            p.Image = Resources.Stone;
                            p.type = "Stone";
                        }
                        else
                        {
                            p.breakable = false;
                            p.Image = Resources.Lava;
                            p.type = "Lava";
                        }
                    }


                    // Sky/SkyClouds (0-3)
                    else if (p.row <= 2)
                    {
                        p.breakable = false;

                        if (rng.Next(0, 2) == 0)
                        {
                            p.Image = Resources.Sky;
                            p.type = "Sky";
                        }
                        else
                        {
                            p.Image = Resources.SkyClouds;
                            p.type = "SkyClouds";
                        }
                    }
                    // Sky (4-8)
                    else if (p.row <= 8)
                    { 
                        p.breakable = false;
                        p.Image = Resources.Sky;
                        p.type = "Sky";
                    }
                    // Grass (9)
                    else if (p.row == 9)
                    {
                        p.Image = Resources.Grass;
                        p.type = "Grass";
                    }
                    // Dirt/Sand (10-12) 10%
                    else if (p.row <= 12)
                    {
                        int random = rng.Next(0, 10);
                        if (random == 0)
                        {
                            p.Image = Resources.Sand;
                            p.type = "Sand";
                        }
                        else
                        {
                            p.Image = Resources.Dirt;
                            p.type = "Dirt";
                        }
                    }
                    // Dirt/Gravel (13-14) 33%
                    else if (p.row <= 14)
                    {
                        int random = rng.Next(0, 3);
                        if (random == 0)
                        {
                            p.Image = Resources.Gravel;
                            p.type = "Gravel";
                        }
                        else
                        {
                            p.Image = Resources.Dirt;
                            p.type = "Dirt";
                        }
                    }
                    // Gravel/Stone (15-18)
                    // 15-16, 33%
                    // 17-18, 25%
                    else if (p.row <= 16)
                    {
                        int random = rng.Next(0, 3);
                        if (random == 0)
                        {
                            p.Image = Resources.Gravel;
                            p.type = "Gravel";
                        }
                        else
                        {
                            p.Image = Resources.Stone;
                            p.type = "Stone";
                        }
                    }
                    else if (p.row <= 18)
                    {
                        int random = rng.Next(0, 4);
                        if (random == 0)
                        {
                            p.Image = Resources.Gravel;
                            p.type = "Gravel";
                        }
                        else
                        {
                            p.Image = Resources.Stone;
                            p.type = "Stone";
                        }
                    }
                    // Stone/Diamond 1% (19-22)
                    else if (p.row <= 22)
                    {
                        if (rng.Next(0, 100) == 0)
                        { 
                            p.Image = Resources.Diamond; 
                            p.type = "Diamond"; 
                        }
                        else 
                        { 
                            p.Image = Resources.Stone; 
                            p.type = "Stone"; 
                        }
                    }
                    // Stone/Diamond 5%
                    else if (p.row <= boxesVertically - 4)
                    {
                        int random = rng.Next(0, 20);
                        if (random == 0)
                        {
                            p.Image = Resources.Diamond;
                            p.type = "Diamond";
                        }
                        else 
                        {
                            p.Image = Resources.Stone;
                            p.type = "Stone";
                        }
                    }
                    // Stone/Diamond ~30% 
                    else if (p.row == boxesVertically - 3)
                    {
                        int random = rng.Next(0, 7);
                        if (random == 0)
                        {
                            p.Image = Resources.Diamond;
                            p.type = "Diamond";
                        }
                        else
                        {
                            p.Image = Resources.Stone;
                            p.type = "Stone";
                        }
                    }

                    // Type of block to array with the same size as the
                    // array containing PicBoxes, e.g. types[0,0] is the type
                    // that picBoxes[0, 0] contains.
                    types[p.row, p.column] = p.type;

                    p.Click += Click;

                    if (p.breakable == true)
                    {
                        p.Cursor = Cursors.Hand;
                    }

                    boxesAdded++;

                    if (boxesAdded == boxesVertically)
                    {
                        columnsAdded++;
                        boxesAdded = 0;
                    }
                }
            }

            SetCharacterAndScore();
        }

        private void SetCharacterAndScore()
        {
            character = new Character(Resources.Character, picSize);

            double left = boxesHorizontally / 2.0;

            character.Top = 0;
            character.Left = (int)left * picSize;

            character.detector.Top = character.Height;
            character.detector.Left = character.Left;

            labelScore.Left = 0;
            labelScore.Top = 0;
            labelScore.Width = picSize * 2 + 10;
            labelScore.Height = picSize - 4;
            labelScore.BackColor = Color.Transparent;

            labelScore.Text = "Score: " + 0;

            Controls.Add(character);
            Controls.Add(character.detector);
            Controls.Add(labelScore);

            character.BringToFront();
            labelScore.BringToFront();

            OnSky();
        }

        private void Click(object sender, EventArgs e)
        {
            if (sender is PicBox)
            {
                MouseEventArgs E = (MouseEventArgs)e;

                PicBox p = (PicBox)sender;

                if (E.Button == MouseButtons.Right)
                {
                    if (controller.BesidesBlock(p.row, p.column, "Grass/Dirt/Sand/Gravel/Stone/Diamond/Lava"))
                    {
                        if (p.type == "Sky" || p.type == "SkyClouds")
                        {
                            p.Image = Resources.Stone;
                            p.type = "Stone";
                            types[p.row, p.column] = p.type;

                            p.breakable = true;
                            p.Cursor = Cursors.Hand;
                        }
                    }
                }
                else if (controller.BesidesBlock(p.row, p.column, "Sky"))
                {
                    if (p.type == "Diamond") { labelScore.Text = "Score " + controller.IncreaseAndGetScore(); }

                    p.Image = Resources.Sky;
                    p.type = "Sky";
                    types[p.row, p.column] = p.type;
                    p.Cursor = Cursors.Default;
                }
            }

            OnSky();
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) { UpdateCharacter("A"); }
            else if (e.KeyCode == Keys.D) { UpdateCharacter("D"); }
            else if (e.KeyCode == Keys.W) { UpdateCharacter("W");  }
        }

        private void UpdateCharacter(string key)
        {
            if (key == "D" && BlockCheck(key) == "notObstructed")
            {
                character.Left += picSize;
                character.detector.Left += picSize;

                // If the character is in the middle of a jump, they 
                // shouldn't be dragged down to the ground just because 
                // of moving to the left or right
                if (timerJump.Enabled == false) { OnSky(); }
            }
            else if (key == "A" && BlockCheck(key) == "notObstructed")
            {
                character.Left -= picSize;
                character.detector.Left -= picSize;

                if (timerJump.Enabled == false) { OnSky(); }
            }
            else if (key == "D" && BlockCheck(key) == "stepUpD")
            { 
                character.Top -= picSize;
                character.detector.Top -= picSize;
                character.Left += picSize;
                character.detector.Left += picSize;
            }
            else if (key == "A" && BlockCheck(key) == "stepUpA")
            {
                character.Top -= picSize;
                character.detector.Top -= picSize;
                character.Left -= picSize;
                character.detector.Left -= picSize;
            }                                              // You can not jump in the middle of a jump
            else if (key == "W" && BlockCheck(key) == "JumpAble" && timerJump.Enabled == false)
            {
                character.Top -= picSize;
                character.detector.Top -= picSize;
                timerJump.Start();
            }
        }

        private void OnSky()
        {
            PictureBox detect = character.detector;

            int row = detect.Top / picSize;
            int column = detect.Left / picSize;

            while (types[row, column] == "Sky" || types[row, column] == "SkyClouds" || types[row, column] == "Lava")
            {
                detect.Top += picSize;
                character.Top += picSize;

                row = detect.Top / picSize;
                column = detect.Left / picSize;
            }

            if (types[row - 1, column] == "Lava")
            {
                MessageBox.Show("You fell into lava!");
                this.Hide();
                menu.Show();
            }
        }

        private string BlockCheck(string key)
        {
            PictureBox detect = character.detector;

            int row = detect.Top / picSize;
            int column = detect.Left / picSize;


            // Can return:
            // "Obstructed": Character will not move
            // "notObstructed": Character will move 
            // "stepUpD" / "stepUpA": Character will move to right (D) or left (A), and up
            // "JumpAble": Character will jump

            string Ob = "Obstructed";
            string nOb = "notObstructed";
            string stD = "stepUpD";
            string stA = "stepUpA";
            string JA = "JumpAble";


            // Check if character is at the edge of the map
            // If at the right edge and key is "D", Obstructed
            string blockRight = "";
            string blockRightUp = "";
            string blockRightUpUp = "";
            if (key == "D")
            {
                try
                {
                    blockRight = types[row - 1, column + 1];
                    blockRightUp = types[row - 2, column + 1];
                    blockRightUpUp = types[row - 3, column + 1];
                }
                catch (IndexOutOfRangeException)
                {
                    return Ob;
                }
            }

            // If at the left edge and key is "A", Obstructed
            string blockLeft = "";
            string blockLeftUp = "";
            string blockLeftUpUp = "";
            if (key == "A")
            {
                try
                {
                    blockLeft = types[row - 1, column - 1];
                    blockLeftUp = types[row - 2, column - 1];
                    blockLeftUpUp = types[row - 3, column - 1];
                }
                catch (IndexOutOfRangeException)
                {
                    return Ob;
                }
            }

            string blockAbove = "";
            if (key == "W")
            {
                try
                {
                    blockAbove = types[row - 3, column];
                }
                catch (IndexOutOfRangeException)
                {
                    return Ob;
                }
            }



            if (key == "D")
            {
                // Check blocks right of character
                try
                {
                    // notObstructed
                    if (blockRight == "Sky" && blockRightUp == "Sky")
                    {
                        return nOb;
                    }
                    // stepUpD                                           You can not stepUp in the middle of a jump
                    else if (blockRightUp == "Sky" && blockRightUpUp == "Sky" && timerJump.Enabled == false)
                    {
                        return stD;
                    }
                    // Obstructed
                    else 
                    {
                        return Ob;
                    }
                }
                catch (IndexOutOfRangeException) // If trying to walk off map
                {
                    return Ob;
                }
            }
            else if (key == "A")
            {
                // Check blocks left of character
                try
                { 
                    // notObstructed
                    if (blockLeft == "Sky" && blockLeftUp == "Sky")
                    {
                        return nOb;
                    }
                    // stepUpA
                    else if (blockLeftUp == "Sky" && blockLeftUpUp == "Sky" && timerJump.Enabled == false)
                    {
                        return stA;
                    }
                    // Obstructed
                    else
                    {
                        return Ob;
                    }
                }
                catch (IndexOutOfRangeException) // If trying to walk off map
                {
                    return Ob;
                }
            }
            
            if (key == "W")
            {
                // JumpAble
                if (blockAbove == "Sky" || blockAbove == "SkyClouds")
                {
                    return JA;
                }
                // Obstructed
                else
                {
                    return Ob;
                }
            }

            if (key == "RMB")
            {

            }
            // Obstructed
            return Ob;
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }

        private void timerJump_Tick(object sender, EventArgs e)
        {
            OnSky();
            timerJump.Stop();
        }
    }
}