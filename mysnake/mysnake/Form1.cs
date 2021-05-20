using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mysnake
{
    public partial class Form1 : Form
    {
        private Label labelScore;
        private int width = 600;
        private int height = 600;
        private int sizesnake = 40;
        private int forX, forY;
        private int rX, rY;
        private int rXX, rYY;
        private PictureBox fruit;
        private PictureBox bomb;
        private PictureBox[] snake = new PictureBox[400];
        private int score = 0;
        int h, m, s;
        int p = 0;
        private Panel controlPanel;


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Form1()
        {

            InitializeComponent();
            this.Width = width + 60;
            this.Height = height + 120;

            controlPanel = new Panel();
            Map map = new Map(width,  height,  sizesnake, controlPanel);




            timer2.Interval = 1000;
            h = 0; m = 0;  s = 0;  

            label1.Text = "00 :";
            label2.Text = "00";
            label3.Text = "00 :";
            timer2.Start();

            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Location = new Point(this.Width / 2 - 30, 40);
            this.Controls.Add(labelScore);

            snake[0] = new PictureBox();
            snake[0].Location = new Point(400, 400);
            snake[0].Size = new Size(sizesnake, sizesnake);
            snake[0].BackColor = Color.Green;
            this.Controls.Add(snake[0]);

            forX = 1;
            forY = 0;


            timer1.Tick += new EventHandler(update);
            timer1.Interval = 300;
            timer1.Start();
            this.KeyDown += new KeyEventHandler(Run);


            bomb = new PictureBox();
            bomb.BackColor = Color.Black;
            bomb.Size = new Size(sizesnake, sizesnake);
            Bomb();



            fruit = new PictureBox();
            fruit.BackColor = Color.Red;
            fruit.Size = new Size(sizesnake, sizesnake);
            Fruit();

        }
        private void timer2_Tick(object sender, EventArgs e)
        {

            if (s < 59)
            {
                s++;
                if (s < 10)
                    label2.Text = "0" + s.ToString();
                else
                    label2.Text = s.ToString();
            }
            else
            {
                if (m < 59)
                {
                    m++;
                    if (m < 10)
                        label1.Text = "0" + m.ToString() + ":";
                    else
                        label1.Text = m.ToString() + ":";
                    s = 0;
                    label2.Text = "00";

                }
                else
                {
                    m = 0;
                    label1.Text = "00 :";
                }
            }
            if (m==59 && s==59)
            {
                h++;
                if (h < 10)
                    label3.Text = "0" + h.ToString() + ":";
                else
                    label3.Text = h.ToString() + ":";

            }
        }

        private void update(Object myObject, EventArgs eventsArgs)
        {
            eatFruit();
            moveSnake();
            eatItself();

        }

        private void Run(object sender, KeyEventArgs a)
        {
            switch (a.KeyCode.ToString())
            {
                case "Right":

                    forX = 1;
                    forY = 0;

                    break;

                case "Left":

                    forX = -1;
                    forY = 0;

                    break;
                case "Up":

                    forY = -1;
                    forX = 0;

                    break;
                case "Down":

                    forY = 1;
                    forX = 0;

                    break;
            }

        }

        private void Bomb()
        {

            Random a = new Random();
            rXX = a.Next(40, width - 40);
            int tempII = rXX % sizesnake;
            rXX -= tempII;
            rYY = a.Next(80, width - 40);
            int tempJJ = rYY % sizesnake;
            rYY -= tempJJ;
            if (rX == rXX && rY == rYY)
            {
                Bomb();
            }
            for (int k = 0; k <= score; k++)
            {
                if (snake[k].Location.X == rXX && snake[k].Location.Y == rYY )
                {
                    Bomb();
                }
            }
            bomb.Location = new Point(rXX, rYY);
            this.Controls.Add(bomb);
        }

        private void Fruit()
        {
            Random r = new Random();
            rX = r.Next(40, width - 40);
            int tempI = rX % sizesnake;
            rX -= tempI;
            rY = r.Next(80, width - 40);
            int tempJ = rY % sizesnake;
            rY -= tempJ;

            for (int i = 0; i <= score; i++)
            {
                if (snake[i].Location.X == rX && snake[i].Location.Y == rY || rX == rXX && rY== rYY)
                {
                    Fruit();
                }
            }

             fruit.Location = new Point(rX, rY);
             this.Controls.Add(fruit);
            
        }



        private void eatFruit()
        {
            if (score == 195)
            {
                timer1.Stop();
                Form3 form3 = new Form3();
                form3.Show();
            }
            if (snake[0].Location.X == rX && snake[0].Location.Y == rY)
            {
                labelScore.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * forX, snake[score - 1].Location.Y - 40 * forY);
                snake[score].Size = new Size(sizesnake, sizesnake);
                snake[score].BackColor = Color.Green;
                this.Controls.Add(snake[score]);
                Fruit();

            }
            if (snake[0].Location.X == rXX && snake[0].Location.Y == rYY)
            {
                timer1.Stop();
                Form2 form2 = new Form2();
                form2.Show();

            }
            if (score % 5 == 0 && score != p && score > 0 && score <70)
            {
                p = score;
                Bomb();
            }
            if (score == 70)
            {
                bomb.Visible=false;
                rYY = 0;
                rXX = 0;
            }
        }



        private void moveSnake()
        {
            for (int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }

            snake[0].Location = new Point(snake[0].Location.X + forX * sizesnake, snake[0].Location.Y + forY * sizesnake);

            if (snake[0].Location.X < 40 )
            {
                snake[0].Location = new Point(560, snake[0].Location.Y + forY * sizesnake);

            }
            if (snake[0].Location.X > 560 )
            {
                snake[0].Location = new Point(40, snake[0].Location.Y + forY * sizesnake);
            }

            if (snake[0].Location.Y < 80 )
            {
                snake[0].Location = new Point(snake[0].Location.X + forX * sizesnake, 600);
            }

            if (snake[0].Location.Y > 600 )
            {
                snake[0].Location = new Point(snake[0].Location.X + forX * sizesnake, 80);
            }


        }

        private void eatItself()
        {  
            if (score >= 2)
            {

                if (snake[0].Location == snake[2].Location)
                {
                    for (int j = 1; j <= score; j++)
                        this.Controls.Remove(snake[j]);
                    score = 0;
                    labelScore.Text = "Score: " + score;
                }
            }
            for (int i = 1; i <= score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                    for (int j = i; j <= score; j++)
                        this.Controls.Remove(snake[j]);
                    score = score - (score - i + 1);
                    labelScore.Text = "Score: " + score;
                }

            }


        }


    }
}
