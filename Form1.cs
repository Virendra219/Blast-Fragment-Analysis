using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox oG1; // picturebox containing original image
        Size picSize; // size of input image
        static int w, h, r = 0; //w, h = number of pixels along width, height of the image respt., r = zoomer value;
        byte[,] Grey, Blur; // matrix of pixel byte values after applying greyscale, blur filters resp.
        double[,] Mag, Grad; // matrix containing magnitudes, gradients resp. after applying sobel filter
        Bitmap BMOriginal, BMEdit, BMOg3, BMOg4; // Bitmaps of original image
        double A, B, C, D, E, F, G, H; // variables to store angles of diiferent measures at 45 deg interval

        PictureBox org = new();

        private void Form1_Load(object sender, EventArgs e)
        {
            A = Math.PI / 8;
            B = 3 * A;
            C = 5 * A;
            D = 7 * A;
            E = 9 * A;
            F = 11 * A;
            G = 13 * A;
            H = 15 * A;

            // initialising zoomer values
            zoomer.Minimum = 0;
            zoomer.Maximum = 5;
            zoomer.SmallChange = 1;
            zoomer.LargeChange = 1;
            zoomer.UseWaitCursor = false;

            this.DoubleBuffered = true;
        }

        private void zoomer_Scroll(object sender, EventArgs e)
        {
            r = zoomer.Value;
            if (zoomer.Value != 0)
            {
                Size nsize = new Size((int)(org.Size.Width * (1 + (double)(r) / 2)), (int)(org.Size.Height * (1 + (double)(r) / 2)));
                editImg.Size = nsize;
            }
            else
            {
                editImg.Size = org.Size;
            }
        }

        private void GetImg_Click(object sender, EventArgs e)
        {
            tabPage1.Controls.Clear();
            oG1 = new();
            tabPage1.Controls.Add(oG1);
            oG1.SizeMode = PictureBoxSizeMode.Zoom;
            oG1.Size = new Size(tabPage1.Width, tabPage1.Height);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                oG1.Load(openFileDialog1.FileName);
            }
        }

        // function to convert image to Grayscale
        private void GrayScale()
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Color clr = BMOriginal.GetPixel(j, i);
                    Grey[i, j] = (byte)((clr.R + clr.G + clr.B) / 3);
                }
            }
        }

        // function to appply Gaussian blur filter on Image
        private void GaussianBlur()
        {
            for (int i = 1; i < h - 1; i++)
            {
                for (int j = 1; j < w - 1; j++)
                {
                    Blur[i, j] = (byte)((Grey[i - 1, j - 1] + Grey[i - 1, j + 1] + Grey[i + 1, j - 1] + Grey[i + 1, j + 1] + 2 * (Grey[i, j - 1] + Grey[i, j + 1] + Grey[i - 1, j] + Grey[i + 1, j]) + 4 * Grey[i, j]) / 16);
                }
            }
        }

        // function to apply Sobel operator
        private void SobelOperator()
        {
            // applying Sobel operator and storing gradient, magnitude values 
            for (int i = 1; i < h - 1; i++)
            {
                for (int j = 1; j < w - 1; j++)
                {
                    double Sx = (Blur[i - 1, j + 1] + Blur[i + 1, j + 1] + 2 * Blur[i, j + 1] - Blur[i - 1, j - 1] - Blur[i - 1, j + 1] - 2 * Blur[i, j - 1]) / 4;
                    double Sy = (Blur[i + 1, j - 1] + Blur[i + 1, j + 1] + 2 * Blur[i + 1, j] - Blur[i - 1, j - 1] - Blur[i - 1, j + 1] - 2 * Blur[i - 1, j]) / 4;
                    double S = Math.Sqrt((Sx * Sx + Sy * Sy) / 2);
                    Mag[i, j] = S;
                    double grad;
                    if (Sx == 0)
                    {
                        if (Sy >= 0)
                            grad = Math.PI / 2;
                        else
                            grad = 3 * Math.PI / 2;
                    }
                    else if (Sx > 0 && Sy >= 0)
                    {
                        grad = Math.Atan(Sy / Sx);
                    }
                    else if (Sy >= 0)
                    {
                        grad = Math.PI - Math.Atan(Sy / (-1 * Sx));
                    }
                    else if (Sx > 0)
                    {
                        grad = 2 * Math.PI - Math.Atan((-1 * Sy) / Sx);
                    }
                    else
                    {
                        grad = Math.PI + Math.Atan(Sy / Sx);
                    }
                    Grad[i, j] = grad;
                }
            }
        }

        TabPage SobelImg1 = new("Sobel Image");
        int[,] Dark1;

        private void ShowOutlines_Click(object sender, EventArgs e)
        {
            picSize = oG1.Image.Size;
            tabControl1.SelectedTab = tabPage1;
            w = oG1.Image.Width;
            h = oG1.Image.Height;
            Grey = new byte[h, w];
            Blur = new byte[h, w];
            Mag = new double[h, w];
            Grad = new double[h, w];
            BMOriginal = new(oG1.Image);
            BMOg3 = new(oG1.Image);

            GrayScale();
            GaussianBlur();
            SobelOperator();

            Dark1 = new int[h, w];
            if (tabControl1.Contains(SobelImg1))
            {
                SobelImg1.Controls.Clear();
                tabControl1.Controls.Remove(SobelImg1);
            }
            int tr1 = Convert.ToInt32(t1.Text); // threshold for Sobel operator
            tabControl1.Controls.Add(SobelImg1);
            PictureBox sobelImg1 = new();

            SobelImg1.Controls.Add(sobelImg1);
            sobelImg1.Size = oG1.Size;
            sobelImg1.SizeMode = PictureBoxSizeMode.Zoom;
            Bitmap BMSobel1 = new Bitmap(w, h);

            // checking the magnitude of darkness of each pixel and making them black or white based on threshold value
            for (int i = 1; i < h - 1; i++)
            {
                for (int j = 1; j < w - 1; j++)
                {                
                    if (Mag[i, j] > tr1)
                    {
                        BMSobel1.SetPixel(j, i, Color.FromArgb(255,0,0));
                        Dark1[i, j] = 1;
                    }
                    else
                    {
                        BMSobel1.SetPixel(j, i, BMOriginal.GetPixel(j, i));
                        Dark1[i, j] = 0;
                    }
                }
            }
            Thinner(BMSobel1); // thinning the edges to single pixel width
            sobelImg1.Image = BMSobel1; // assigning thinned image to picture box
            tabControl1.SelectTab(SobelImg1); // sobel image tab gets displayed
        }

        List<int[]> Darks = new(); // List of Dark point coordinates

        // function to thin the borders to single pixel width
        public void Thinner(Bitmap BM)
        {
            Darks = new();
            // traverse through the matrix left after applying Sobel operator
            // on encountering a dark point, traverse along the direction perpendicular to that pixel and mark all these pixels white except the darkest one
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (Dark1[i, j] == 1)
                    {
                        if ((Grad[i, j] >= 0 && Grad[i, j] <= A) || (Grad[i, j] >= D && Grad[i, j] <= E) || Grad[i, j] >= H)
                        {
                            int b = j + 1;
                            int y = j;
                            Dark1[i, j] = 0;
                            BM.SetPixel(j, i, BMOriginal.GetPixel(j, i));
                            while (b < w && Dark1[i, b] == 1)
                            {
                                Dark1[i, b] = 0;
                                BM.SetPixel(b, i, BMOriginal.GetPixel(b, i));
                                if (Mag[i, b] > Mag[i, y])
                                {
                                    y = b;
                                }
                                b++;
                            }
                            int[] darkP = { i, y };
                            Darks.Add(darkP);
                            Dark1[i, y] = 1;
                            BM.SetPixel(y, i, Color.FromArgb(255,0,0));
                        }
                        else if ((Grad[i, j] >= A && Grad[i, j] <= B) || (Grad[i, j] >= E && Grad[i, j] <= F))
                        {
                            int c = i + 1, d = j + 1;
                            Dark1[i, j] = 0;
                            BM.SetPixel(j, i, BMOriginal.GetPixel(j, i));
                            int x = i, y = j;
                            while (c < h && d < w && Dark1[c, d] == 1)
                            {
                                Dark1[c, d] = 0;
                                BM.SetPixel(d, c, BMOriginal.GetPixel(d, c));
                                if (Mag[c, d] > Mag[x, y])
                                {
                                    x = c;
                                    y = d;
                                }
                                c++;
                                d++;
                            }
                            int[] darkP = { x, y };
                            Darks.Add(darkP);
                            Dark1[x, y] = 1;
                            BM.SetPixel(y, x, Color.FromArgb(255,0,0));
                        }
                        else if ((Grad[i, j] >= B && Grad[i, j] <= C) || (Grad[i, j] >= F && Grad[i, j] <= G))
                        {
                            int b = i + 1;
                            Dark1[i, j] = 0;
                            BM.SetPixel(j, i, BMOriginal.GetPixel(j, i));
                            int x = i;
                            while (b < h && Dark1[b, j] == 1)
                            {
                                Dark1[b, j] = 0;
                                BM.SetPixel(j, b, BMOriginal.GetPixel(j, b));
                                if (Mag[x, j] < Mag[b, j])
                                {
                                    x = b;
                                }
                                b++;
                            }
                            int[] darkP = { x, j };
                            Darks.Add(darkP);
                            Dark1[x, j] = 1;
                            BM.SetPixel(j, x, Color.FromArgb(255,0,0));
                        }
                        else
                        {
                            Dark1[i, j] = 0;
                            BM.SetPixel(j, i, BMOriginal.GetPixel(j, i));
                            int a = i - 1, b = j + 1;
                            int x = i, y = j;
                            while (a >= 0 && b < w && Dark1[a, b] == 1)
                            {
                                Dark1[a, b] = 0;
                                BM.SetPixel(b, a, BMOriginal.GetPixel(b, a));
                                if (Mag[a, b] > Mag[x, y])
                                {
                                    x = a;
                                    y = b;
                                }
                                b++;
                                a--;
                            }
                            int[] darkP = { x, y };
                            Darks.Add(darkP);
                            Dark1[x, y] = 1;
                            BM.SetPixel(y, x, Color.FromArgb(255,0,0));
                        }
                    }
                }
            }
        }

        // Shifting an image to editor where it could be worked upon by the user
        private void Shift_to_editor_Click(object sender, EventArgs e)
        {
            zoomer.Value = 0;   
            editImg.Image = null;
            editImg.Size = picSize;
            org = new();
            org.Size = picSize;
            editImg.SizeMode = PictureBoxSizeMode.Zoom;
            org.SizeMode = PictureBoxSizeMode.Zoom;
            foreach (PictureBox pb3 in tabControl1.SelectedTab.Controls)
                editImg.Image = pb3.Image;
            org.Image = editImg.Image;
            BMEdit = new(editImg.Image);
            tabControl1.SelectTab(Editor);

            bool[,] visited = new bool[h, w];

            // using dfs to make a list of list of pixels of each segment in the image
            for (int i = 0; i < Darks.Count; i++)
            {
                if (!visited[Darks[i][0], Darks[i][1]])
                {
                    Color sclr = Color.FromArgb(255, 0, 0);
                    seg = new();
                    markBoundary(visited, Darks[i][0], Darks[i][1], sclr);
                    if (seg.Count == 0)
                        continue;
                    segments.Add(seg);
                }
            }
           
            editImg.Image = BMEdit;
            org.Image = BMEdit;
            BMOg4 = new(BMEdit);
        }

        int Tr1 = 0;
        // function to remove segments with number of pixels below given threshold
        // also hendles cases where thresolds are changed to a higher or lower values
        private void Remove_noise_Click(object sender, EventArgs e)
        {
            int Tr2 = Convert.ToInt32(t2.Text);
            if (Tr2 == Tr1)
                return;
            for (int i = 0; i<segments.Count; i++)
            {
                if (Tr2 > Tr1)
                {
                    if (segments[i].Count > Tr1 && segments[i].Count < Tr2)
                    {
                        for (int j = 0; j < segments[i].Count; j++)
                        {
                            BMEdit.SetPixel(segments[i][j][1], segments[i][j][0], BMOg3.GetPixel(segments[i][j][1], segments[i][j][0]));
                        }
                    }
                }
                else
                {
                    if (segments[i].Count > Tr2 && segments[i].Count < Tr1)
                    {
                        for (int j = 0; j < segments[i].Count; j++)
                        {
                            BMEdit.SetPixel(segments[i][j][1], segments[i][j][0], BMOg4.GetPixel(segments[i][j][1], segments[i][j][0]));
                        }
                    }
                }
            }
            editImg.Image = BMEdit;
            Tr1 = Tr2;
        }
        

        List<List<int[]>> segments = new();
        List<int[]> terminals = new();
        List<int[]> seg = new();
        void markBoundary(bool[,] visited, int i, int j, Color sclr)
        {
            if (i < 0 || j < 0 || i >= Dark1.GetLength(0) || j >= Dark1.GetLength(1) || Dark1[i, j] == 0 || visited[i, j])
                return;
            visited[i, j] = true;
            BMEdit.SetPixel(j, i, sclr);
            int[] t = { i, j };
            seg.Add(t);
            markBoundary(visited, i - 1, j - 1, sclr);
            markBoundary(visited, i - 1, j, sclr);
            markBoundary(visited, i - 1, j + 1, sclr);
            markBoundary(visited, i, j - 1, sclr);
            markBoundary(visited, i, j + 1, sclr);
            markBoundary(visited, i + 1, j - 1, sclr);
            markBoundary(visited, i + 1, j, sclr);
            markBoundary(visited, i + 1, j + 1, sclr);
        }

        int flag = 0; // Semaphore to keep a check of which functionality should be used on clicking on the image

        // functionality 1, Erase function to erase noise
        // sets flag to 1
        private void Erase_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                flag = 0;
                editImg.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                flag = 1;
                editImg.Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        // functionality 2, draw function to complete edges
        // sets flag to 2
        private void Draw_Click(object sender, EventArgs e)
        {
            if (flag == 2)
            {
                flag = 0;
                editImg.Cursor = System.Windows.Forms.Cursors.Default;
                Grp.Dispose();
            }
            else
            {
                flag = 2;
                Grp = Graphics.FromImage(BMEdit);
                Grp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                redPen.StartCap = redPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                editImg.Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }
        // functionality 3, break junction function to break the junctions in the edges if necessary
        // sets flag to 3
        private void Break_Jn_Click(object sender, EventArgs e)
        {
            if (flag == 3)
            {
                flag = 0;
                editImg.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                flag = 3;
                editImg.Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }
        // functionality 4, mark shadow function to assign black colour to the shadows
        // sets flag to 4
        private void Mark_Shadow_Click(object sender, EventArgs e)
        {
            if (flag == 4)
            {
                flag = 0;
                editImg.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                flag = 4;
                editImg.Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        List<int[]> terminal = new();
        Pen redPen = new(Color.FromArgb(255,0,0), 2);

        // functionality 5, mark scale function to mark the scale on them image
        // sets flag to 5
        private void markScale_Click(object sender, EventArgs e)
        {
            if (flag == 5)
            {
                flag = 0;
                editImg.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                flag = 5;
                editImg.Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        int x = -1, y = -1;
        bool moving = false;
        Graphics Grp;
        List<int[]> scaleCoordinates = new();

        // click event handler for the image to operate as per the flag value
        private void editImg_MouseClick(object sender, MouseEventArgs e)
        {
            int i = e.Y, j = e.X; // i, j => coordinates of bitmap corresponding to the mouse click point. 

            // adjusting the coordinate in case of zoomed image
            if (r != 0)
            {
                i = (int)(i / (1 + (double)(r) / 2));
                j = (int)(j / (1 + (double)(r) / 2));
            }

            if (flag == 1)
            {
                seg = new();
                // calls the erase function for 3 x 3 points with i, j as the center to account for misclick
                for (int x = i - 1; x <= i + 1; x++)
                {
                    for (int y = j - 1; y <= j + 1; y++)
                    {
                        erase(x, y); 
                    }
                }
            }

            // if user wants to draw a line between 2 points,
            // if a point was already stored, current point will be joined to it
            // otherwise current would be stored
            if (flag==2)
            {
                if (terminal.Count == 1)
                {
                    Grp.DrawLine(redPen, terminal[0][0], terminal[0][1], x, y);
                    editImg.Image = BMEdit;
                    terminal = new();
                }
                else
                {
                    int[] t = { x, y };
                    terminal.Add(t);
                }
            }

            // surrounding of the clicked point is assigned its original color hence erasing the edge demarkation
            if (flag == 3)
            {
                int y1 = Math.Max(0, i - r), x1 = Math.Max(0, j - r), y2 = Math.Min(h - 1, i + r), x2 = Math.Min(w - 1, j + r);
                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        BMEdit.SetPixel(x, y, BMOg3.GetPixel(x, y));
                    }
                }
            }

            // calls the ColorBlack function to mark a closed loop as shadow
            if (flag == 4)
            {
                ColorBlack(i, j);
            }

            // storing the scale coordinates
            if (flag == 5)
            {
                int[] coordinate = { i, j };
                scaleCoordinates.Add(coordinate);
                if (scaleCoordinates.Count == 2)
                {
                    flag = 0;
                    editImg.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
            editImg.Image = BMEdit;
            org.Image = BMEdit;
        }

        // if user wants to draw a continuous curve with hand
        private void editImg_MouseDown(object sender, MouseEventArgs e)
        {
            if (flag == 2)
            {
                x = e.X;
                y = e.Y;
                if (zoomer.Value != 0)
                {
                    x = (int)(e.X / (1 + (double)r / 2));
                    y = (int)(e.Y / (1 + (double)r / 2));
                }
                moving = true;
            }
        }

        private void editImg_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving && flag==2)
            {
                int x1 = e.X, y1 = e.Y;
                if (zoomer.Value != 0)
                {
                    x1 = (int)(e.X / (1 + (double)r / 2));
                    y1 = (int)(e.Y / (1 + (double)r / 2));
                }
                Grp.DrawLine(redPen, x, y, x1, y1);
                x = x1;
                y = y1;
                editImg.Image = BMEdit;
                org.Image = BMEdit;
            }
        }
        private void editImg_MouseUp(object sender, MouseEventArgs e)
        {
            if (flag == 2)
            {
                moving = false;
                x = -1;
                y = -1;
                editImg.Image = BMEdit;
                org.Image = BMEdit;
                terminal = new();
            }
        }

        // function to erase a clicked segment using dfs
        private void erase(int i, int j)
        {
            if (i < 0 || j < 0 || i >= BMEdit.Height || j >= BMEdit.Width)
                return;

            if (BMEdit.GetPixel(j, i) != Color.FromArgb(255, 0, 0))
                return;
            BMEdit.SetPixel(j, i, BMOg3.GetPixel(j, i));
            erase(i - 1, j - 1);
            erase(i - 1, j);
            erase(i - 1, j + 1);
            erase(i, j - 1);
            erase(i, j + 1);
            erase(i + 1, j - 1);
            erase(i + 1, j);
            erase(i + 1, j + 1);
        }
        
        // dfs function to color a clicked closed loop with black color
        private void ColorBlack(int i, int j)
        {
            if (i<0 || j<0 || i>=h || j>=w)
            {
                return;
            }
            if (BMEdit.GetPixel(j, i) == Color.FromArgb(255,0,0) || BMEdit.GetPixel(j,i)==Color.FromArgb(0,0,0))
            {
                return;
            }
            BMEdit.SetPixel(j, i, Color.FromArgb(0,0,0));
            ColorBlack(i - 1, j);
            ColorBlack(i + 1, j);
            ColorBlack(i, j - 1);
            ColorBlack(i, j + 1);
        }

        bool[,] vis = new bool[h, w];
        List<int[]> ColorCounts = new(); // list that stores an arraay of length 4 which stores r, b, g values of colour given to a rock fragment and number of pixels having that color
        byte red = 100, green = 0, blue = 0;

        // event handler for FillColour button
        private void FillColour_Click(object sender, EventArgs e)
        {
            for (int i=0; i<h; i++)
            {
                for (int j=0; j<w; j++)
                {
                    if (BMEdit.GetPixel(j,i)==Color.FromArgb(255,0,0) || BMEdit.GetPixel(j,i)==Color.FromArgb(0,0,0))
                    {
                        vis[i, j] = true;
                    }
                    if (!vis[i, j])
                    {
                        Color fclr = Color.FromArgb(red, green, blue);
                        int[] temp = { red, green, blue, 0 };
                        ColorCounts.Add(temp);
                        Fill(i, j, fclr);
                        blue++;
                        if (blue==100)
                        {
                            blue = 0;
                            green++;
                        }
                        if (green==100)
                        {
                            green = 0;
                            red++;
                        }
                    }
                }
            }
        }
        // dfs function to fill colour into a closed loop and store the number of pixels of a coolour
        private void Fill(int i, int j, Color fclr)
        {
            if (i<0 || j<0 || i>h || j>w || vis[i,j])
            {
                return;
            }
            vis[i, j] = true;
            BMEdit.SetPixel(j, i, fclr);
            ColorCounts.Last()[3]++;
            Fill(i - 1, j, fclr);
            Fill(i + 1, j, fclr);
            Fill(i, j - 1, fclr);
            Fill(i, j + 1, fclr);
        }
        // calculation of blasting efficiency
        private void analyze_Click(object sender, EventArgs e)
        {
            double actualDistance = Convert.ToDouble(scaleVal.Text);
            double imgDistance = Math.Sqrt(Math.Pow(scaleCoordinates[1][1] - scaleCoordinates[0][1], 2) + Math.Pow(scaleCoordinates[1][0] - scaleCoordinates[0][0], 2));
            double pixelArea = actualDistance/imgDistance;
            double areaMin = Convert.ToDouble(minAreaVal.Text), areaMax = Convert.ToDouble(maxAreaVal.Text);
            int acceptable = 0;
            for (int i=0; i<ColorCounts.Count; i++)
            {
                double areaCur = ColorCounts[i][3] * pixelArea;
                if (areaCur >= areaMin && areaCur <= areaMax)
                    acceptable++;
            }
            efficiency.Text = Convert.ToString(acceptable / ColorCounts.Count * 100);
        }
    }
}
