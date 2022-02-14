using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2proyect_DibujoApp
{
    public partial class Form1 : Form
    {
        public Point current = new Point();
        public Point old = new Point();

        public Graphics g;
        public Graphics graph;

        public Pen pen = new Pen(Color.Black, 5);

        Bitmap surface;


        public Form1()
        {
            InitializeComponent();

            g = canvasPanel.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);

            surface = new Bitmap(canvasPanel.Width, canvasPanel.Height);    
            
            graph = Graphics.FromImage(surface);

            canvasPanel.BackgroundImage = surface;
            canvasPanel.BackgroundImageLayout = ImageLayout.None;

            pen.Width = (float)painbrush_size.Width;

;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            old = e.Location;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            //dibujar 

            if(e.Button == MouseButtons.Left)
            {
                current = e.Location;
                g.DrawLine(pen, old, current);
                graph.DrawLine(pen, old, current);

                old = current;

            }
        }
        private Point mouseOffsetPos;
        private Boolean isMouseDown = false;

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                mouseOffsetPos = new Point(e.X, e.Y);   
                isMouseDown = true; 
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffsetPos.X, mousePos.Y);
                this.Location = mousePos;
            }

        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eraser_button_Click(object sender, EventArgs e)
        {
            // borrador 
            pen.Color = Color.White;

        }

        private void paintbrush_button_Click(object sender, EventArgs e)
        {
            // cambiar colores 
            pen.Color = colorbox.BackColor;

        }

        private void colorbox_Click(object sender, EventArgs e)
        {
            // agregar colores 
            ColorDialog cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                pen.Color = cd.Color;   
                colorbox.BackColor = cd.Color;
            }
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            // limpiar todo 
            graph.Clear(Color.White);
            canvasPanel.Invalidate();

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            //guardar

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "png Files (*png) | *png";
            sfd.DefaultExt = "png";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                surface.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }

        }

        private void paintbrush_change(object sender, EventArgs e)
        {
            // grosor del pincel
            pen.Width = (float)painbrush_size.Value;

        }

        private void canvasPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
