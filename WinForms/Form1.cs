using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagGameLib;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private Random _rnd = new Random();
        private readonly ModelGame _model;

        private readonly Button[,] _map = new Button[4, 4];
        
        public Form1()
        {
            InitializeComponent();
            _model = new ModelGame();
            _model.Init();
            _model.RePaint += Model_RePaint;
            this.Paint += Form1_Paint;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var btn = new Button
                    {
                        Text = "",
                        Size = new Size(80, 80),
                        Font = new Font("", 30),
                        Location = new Point(100 + j * 80, 100 + i * 80)
                    };
                    btn.Click += Btn_Click;
                    this.Controls.Add(btn);
                    _map[i, j] = btn;
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _map[i, j].Text = _model[i, j].ToString();
                    _map[i, j].Visible = _model[i, j] != 0;
                }
            }
        }

        private void Model_RePaint(object sender, int[,] map)
        {
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            MoveDirection dir = MoveDirection.None;
            switch (e.KeyCode)
            {
                case Keys.Left: dir = MoveDirection.Left;
                    break;
                case Keys.Right: dir = MoveDirection.Right;
                    break;
                case Keys.Up: dir = MoveDirection.Up;
                    break;
                case Keys.Down: dir = MoveDirection.Down;
                    break;
            }

            if (dir != MoveDirection.None)
            {
                _model.KeyDown(dir);
                return;
            }
            base.OnKeyDown(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            _model.KeyDown(MoveDirection.Left);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _model.KeyDown(MoveDirection.Right);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _model.KeyDown(MoveDirection.Down);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _model.KeyDown(MoveDirection.Up);
        }
    }
}