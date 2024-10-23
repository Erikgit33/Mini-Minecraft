using MVC_Game.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC_Game
{
    internal class Model
    {
        private int score;

        private int boxesHorizontally;
        private int boxesVertically;

        internal PicBox[,] picBoxes;
        public Model (int boxesVertically, int boxesHorizontally)
        {
            this.boxesVertically = boxesVertically;
            this.boxesHorizontally = boxesHorizontally;
        }
        
        private void AddPictureBoxes()
        {
            picBoxes = new PicBox[boxesVertically, boxesHorizontally];

            // Adds a pictureBox to each slot in the array
            for (int indexColumn = 0; indexColumn < boxesHorizontally; indexColumn++)
            {
                for (int indexRow = 0; indexRow < boxesVertically; indexRow++)
                {
                    PicBox pic = new PicBox(indexRow, indexColumn);

                    picBoxes[indexRow, indexColumn] = pic;
                }
            }
        }

        internal PicBox[,] GetGrid()
        {
            AddPictureBoxes();
            return picBoxes;
        }

        internal bool BlockExists(int row, int column)
        {
            try
            {
                if (picBoxes[row, column] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException)
            { 
                return false; 
            }
        }

        internal bool BesidesBlock(int row, int column, string type)
        {
            string[] types = { "Hello" };
            if (type.Contains("/"))
            {
                // Creates an aray with different types when more 
                // than once type is present
                types = type.Split('/');
            }
            else
            {
                types[0] = type;
            }


            if (BlockExists(row, column))
            {
                foreach (string t in types)
                {
                    if (column != boxesHorizontally - 1)
                    {
                        if (picBoxes[row, column + 1].type == t)
                        {
                            return true;
                        }
                    }
                    
                    if (column != 0)
                    {
                        if (picBoxes[row, column - 1].type == t)
                        {
                            return true;
                        }
                    }
                    
                    if (row != 0)
                    {
                        if (picBoxes[row - 1, column].type == t)
                        {
                            return true;
                        }
                    }

                    if (row != boxesVertically - 1)
                    {
                        if (picBoxes[row + 1, column].type == t)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            return false;
        }

        internal int IncreaseScore()
        {
            score++;
            return score;
        }

        
    }
}
