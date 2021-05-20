using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mysnake
{

     class Map
    {
        private int _width;
        private int _height;
        private int _sizesnake;

        public Map(int width, int height, int sizesnake, Panel controlPanel)
        {
            _width = width;
            _height = height;
            _sizesnake = sizesnake;
            controlPanel.Width = _width ;
            controlPanel.Height = _height ;
            CreateMap(controlPanel);
        }

        public void CreateMap(Panel panel)
        {
            for (int i = 2; i <= _width / _sizesnake + 1; i++)
            {
                PictureBox lines = new PictureBox();
                lines.BackColor = Color.Black;
                lines.Location = new Point(40, _sizesnake * i);
                lines.Size = new Size(_width - 40, 1);
                panel.Controls.Add(lines);
            }
            for (int i = 1; i <= _height / _sizesnake; i++)
            {
                PictureBox lines = new PictureBox();
                lines.BackColor = Color.Black;
                lines.Location = new Point(_sizesnake * i, 80);
                lines.Size = new Size(1, _width - 40);
                panel.Controls.Add(lines);
            }
        }
    }      
}
        
