using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap screen;

        private int G_WIDTH = 50;
        private int G_HEIGHT = 50;

        private float distX = 0;
        private float distY = 0;

        public Grid world = new Grid();

        private void Start(object sender, EventArgs e)
        {
            screen = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            world = new Grid(G_WIDTH, G_HEIGHT);

            distX = (pictureBox1.Width / G_WIDTH);
            distY = pictureBox1.Height / G_HEIGHT * (pictureBox1.Width / pictureBox1.Height);

            Render();
        }

        bool CanRender = false;
        bool drawGrid = true;

        private void Ticker_tick(object sender, EventArgs e)
        {
            if (!CanRender)
                return;

        //    Render();
            NextStage();

            Ticker.Interval = (int)((1.0f / simulationSpeedBar.Value) * 1000);
        }

    

        private void Render()
        {

            //clear map
            screen = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            //code
      

            for (int y = 0; y < world.Height; y++)
            {
                for (int x = 0; x < world.Width; x++)
                {
                    if (world.GetCell(x, y) != 0)
                    {
                        PutBox(x, y);
                    }
                }
            }

            if (drawGrid)
                DrawGrid();

            //assign map
            pictureBox1.Image = screen;
        }


        public void PutBox(int x, int y)
        {
            RenderFunctions.DrawRectangle(new Vector2(distX * x, distY * y), distX, distY, Color.Black, screen);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NextStage();
            Render();

         
        }

        void NextStage()
        {
            generation.Text = (Int32.Parse(generation.Text) + 1) + "";
            screen = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            for (int y = 0; y < world.Height; y++)
            {
                for (int x = 0; x < world.Width; x++)
                {
                   
                    if (world.GetCell(x, y) != 0)
                    {
                        //Any live cell with fewer than two live neighbours dies (referred to as underpopulation or exposure).
                        if (world.AliveNeighbours(x, y) < 2)
                            world.SetCell(x, y, 0);

                        //Any live cell with more than three live neighbours dies (referred to as overpopulation or overcrowding).
                        if (world.AliveNeighbours(x, y) > 3)
                            world.SetCell(x, y, 0);

                        //Any live cell with two or three live neighbours lives, unchanged, to the next generation.
                        if (world.AliveNeighbours(x, y) == 2 || world.AliveNeighbours(x, y) == 3)
                            world.SetCell(x, y, 1);

                        PutBox(x, y);
                    }

                    //Any dead cell with exactly three live neighbours will come to life.
                    if (world.GetCell(x, y) == 0)
                    {
                        if (world.AliveNeighbours(x, y) == 3)
                            world.SetCell(x, y, 1);
                    }

                }
            }
            if (drawGrid)
                DrawGrid();
            pictureBox1.Image = screen;
        }

        private void DrawGrid()
        {

            Color color = Color.LightGray;

            //vertical lines
            for (int i = 1; i < G_WIDTH; i++)
            {
                Vector2 start = new Vector2(i * distX, 0);
                Vector2 end = new Vector2(i * distX, pictureBox1.Height);

                RenderFunctions.DrawLine(start, end, color, screen);
            }
            //horizontal
            for (int i = 1; i < G_HEIGHT; i++)
            {
                Vector2 start = new Vector2(0, i * distY);
                Vector2 end = new Vector2(pictureBox1.Width, i * distY);

        

                RenderFunctions.DrawLine(start, end, color, screen);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs cursor = (MouseEventArgs)e;
            Vector2 coord = new Vector2(cursor.Location.X, cursor.Location.Y);

            int numX = (int)(coord.X / distX);
            int numY = (int)(coord.Y / distY);

            world.SetCell(numX, numY, 1);
            Render();

         //   MessageBox.Show(numX+" " + numY);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < world.Width; x++)
            {
                for (int y = 0; y < world.Height; y++)
                {
                    world.SetCell(x, y, 0);
                }
            }

            Render();
            CanRender = false;
            button3.Text = "Start";
            generation.Text = "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!CanRender)
            {
                CanRender = true;
                button3.Text = "Stop";
            }
            else
            {
                CanRender = false;
                button3.Text = "Start";

            }
        }

        private void gridSize_Scroll(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            drawGrid = checkBox1.Checked;
            Render();
        }
    }
}
