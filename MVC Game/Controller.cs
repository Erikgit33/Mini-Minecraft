using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Game
{
    internal class Controller
    {
        private Model model;
        private View view;
        public Controller(Model model, View view ) 
        {
            this.model = model;
            this.view = view;

            view.MakeGridAndSetControl(this);
        }

        public void MakeGrid()
        {
            view.SetGridToView(model.GetGrid());
        }

        public bool BesidesBlock(int row, int column, string type)
        {
            return model.BesidesBlock(row, column, type);
        }

        public int IncreaseAndGetScore()
        {
            return model.IncreaseScore();
        }
    }
}
