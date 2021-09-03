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

        private void GetImg_Click(object sender, EventArgs e)
        {
            Show_Image();
        }
        PictureBox oG1, oG3;
        Size picSize;
        static int w, h, r = 0;
        byte[,] Grey, Blur;
        double[,] Mag, Grad;
        Bitmap BMOriginal, BMEdit, BMOg3, BMOg4;
        double A, B, C, D, E, F, G, H;

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

            zoomer.Minimum = 0;
            zoomer.Maximum = 5;
            zoomer.SmallChange = 1;
            zoomer.LargeChange = 1;
            zoomer.UseWaitCursor = false;

            this.DoubleBuffered = true;
        }

        private void Show_Image()
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
            
            oG3 = new();
            picSize = oG1.Image.Size;
            oG3.Size = picSize;
            oG3.SizeMode = PictureBoxSizeMode.Zoom;
            oG3.Image = oG1.Image;
            tabControl1.SelectedTab = tabPage1;
            w = oG1.Image.Width;
            h = oG1.Image.Height;
            Grey = new byte[h, w];
            Blur = new byte[h, w];
            Mag = new double[h, w];
            Grad = new double[h, w];
            BMOriginal = new(oG1.Image);
            BMOg3 = new(oG3.Image);
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Color clr = BMOriginal.GetPixel(j, i);
                    Grey[i, j] = (byte)((clr.R + clr.G + clr.B) / 3);
                }
            }
            for (int i = 1; i < h - 1; i++)
            {
                for (int j = 1; j < w - 1; j++)
                {
                    Blur[i, j] = (byte)((Grey[i - 1, j - 1] + Grey[i - 1, j + 1] + Grey[i + 1, j - 1] + Grey[i + 1, j + 1] + 2 * (Grey[i, j - 1] + Grey[i, j + 1] + Grey[i - 1, j] + Grey[i + 1, j]) + 4 * Grey[i, j]) / 16);
                }
            }
            TabPage RefImg = new("Reference");
            RefImg.Size = picSize;
            tabControl1.Controls.Add(RefImg);
            RefImg.Controls.Add(oG3);

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

        private void zoomer_Scroll(object sender, EventArgs e)
        {
            r = zoomer.Value;
            if (zoomer.Value != 0)
            {
                Size nsize = new Size((int)(org.Size.Width * (1 + (double)(r) / 2)), (int)(org.Size.Height * (1 + (double)(r) / 2)));
                editImg.Size = nsize;
                oG3.Size = nsize;
            }
            else
            {
                editImg.Size = org.Size;
                oG3.Size = org.Size;
            }
        }
        TabPage SobelImg1 = new("Sobel Image 1");
        int[,] Dark1;

        private void ShowOutlines_Click(object sender, EventArgs e)
        {
            Dark1 = new int[h, w];
            if (tabControl1.Contains(SobelImg1))
            {
                SobelImg1.Controls.Clear();
                tabControl1.Controls.Remove(SobelImg1);
            }
            int tr1 = Convert.ToInt32(t1.Text);
            tabControl1.Controls.Add(SobelImg1);
            PictureBox sobelImg1 = new();

            SobelImg1.Controls.Add(sobelImg1);
            sobelImg1.Size = oG1.Size;
            sobelImg1.SizeMode = PictureBoxSizeMode.Zoom;
            Bitmap BMSobel1 = new Bitmap(w, h), BMSobel2 = new Bitmap(w, h);

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
                        //Color clr = BMOriginal.GetPixel(j, i);
                        BMSobel1.SetPixel(j, i, BMOriginal.GetPixel(j, i));
                        Dark1[i, j] = 0;
                    }
                }
            }
            Thinner(BMSobel1);
            sobelImg1.Image = BMSobel1;
            tabControl1.SelectTab(SobelImg1);
        }
        List<int[]> Darks = new();
        public void Thinner(Bitmap BM)
        {
            Darks = new();
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
                            //BM.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                            while (b < w && Dark1[i, b] == 1)
                            {
                                Dark1[i, b] = 0;
                                BM.SetPixel(b, i, BMOriginal.GetPixel(b, i));
                                //BM.SetPixel(b, i, Color.FromArgb(255, 255, 255));
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
                            //BM.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                            int x = i, y = j;
                            while (c < h && d < w && Dark1[c, d] == 1)
                            {
                                Dark1[c, d] = 0;
                                BM.SetPixel(d, c, BMOriginal.GetPixel(d, c));
                                //BM.SetPixel(d, c, Color.FromArgb(255, 255, 255));
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
                            //BM.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                            int x = i;
                            while (b < h && Dark1[b, j] == 1)
                            {
                                Dark1[b, j] = 0;
                                BM.SetPixel(j, b, BMOriginal.GetPixel(j, b));
                                //BM.SetPixel(j, b, Color.FromArgb(255, 255, 255));
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
                            //BM.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                            int a = i - 1, b = j + 1;
                            int x = i, y = j;
                            while (a >= 0 && b < w && Dark1[a, b] == 1)
                            {
                                Dark1[a, b] = 0;
                                BM.SetPixel(b, a, BMOriginal.GetPixel(b, a));
                                //BM.SetPixel(b, a, Color.FromArgb(255, 255, 255));
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

        private void Shift_to_editor_Click(object sender, EventArgs e)
        {
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

            /*for (int i = 0; i < Darks.Count; i++)
            {
                int c=0;
                int a = Darks[i][0], b = Darks[i][1];
                if (a - 1 >= 0)
                {
                    c += (Dark1[a - 1, b] == 1) ? 1 : 0;
                    if (b - 1 >= 0)
                    {
                        c += ((Dark1[a, b - 1] == 1) ? 1 : 0);
                    }
                    if (b + 1 < w)
                    {
                        c += ((Dark1[a, b + 1] == 1) ? 1 : 0);
                    }
                }
                if (a + 1 < h)
                {
                    c += (Dark1[a + 1, b] == 1) ? 1 : 0;
                }
                if (c>2)
                {
                    Dark1[a, b] = 0;
                    BMEdit.SetPixel(b, a, BMOg3.GetPixel(b,a));
                    Darks.RemoveAt(i);
                    i--;
                }
            }*/
            sw.WriteLine(Convert.ToString(h) + "," + Convert.ToString(w));
            bool[,] visited = new bool[h, w];
            /*for (int i = 1; i <= h + 2; i++)
            {
                visited[i, 1] = true;
                visited[i, w + 1] = true;
            }
            for (int i = 1; i <= w + 2; i++)
            {
                visited[1, i] = true;
                visited[h + 1, i] = true;
            }
            for (int i = 0; i < h + 3; i++)
            {
                for (int j = 0; j < w + 3; j++)
                {
                    sw.Write(Convert.ToString(visited[i, j]) + ",");
                }
                sw.WriteLine(" ");
            }*/
            byte t = 10;
            byte red = t, green = t, blue = t;
            for (int i = 0; i < Darks.Count; i++)
            {
                if (!visited[Darks[i][0], Darks[i][1]])
                {
                    Color sclr = Color.FromArgb(red, green, blue);
                    noise = new();
                    cleanse(visited, Darks[i][0], Darks[i][1], sclr);
                    blue += 10;
                    if (blue > 245)
                    {
                        blue = t;
                        green += 10;
                        if (green > 245)
                        {
                            green = t;
                            red += 10;
                        }
                        if (red > 245)
                        {
                            t++;
                            red = t;
                            green = t;
                            blue = t;
                        }
                    }
                    if (noise.Count == 0)
                        continue;
                    segments.Add(noise);
                    for (int k = 0; k < noise.Count; k++)
                        sw.WriteLine(Convert.ToString(noise[k][0]) + "," + Convert.ToString(noise[k][1]));
                    sw.WriteLine(" ");
                }
            }
            /*GetTerminals();
            segments = new();
            visited = new bool[h, w];
            t = 10;
            red = t;
            green = t;
            blue = t;
            for (int i = 0; i < terminals.Count; i++)
            {
                BMEdit.SetPixel(terminals[i][1], terminals[i][0], Color.FromArgb(0, 0, 0));
                if (!visited[terminals[i][0], terminals[i][1]])
                {
                    Color sclr = Color.FromArgb(red, green, blue);
                    noise = new();
                    cleanse(visited, terminals[i][0], terminals[i][1], sclr);
                    blue += 10;
                    if (blue > 245)
                    {
                        blue = t;
                        green += 10;
                        if (green > 245)
                        {
                            green = t;
                            red += 10;
                        }
                        if (red > 245)
                        {
                            t++;
                            red = t;
                            green = t;
                            blue = t;
                        }
                    }
                    if (noise.Count > 0)
                        segments.Add(noise);
                }
            }*/

            editImg.Image = BMEdit;
            org.Image = BMEdit;
            BMOg4 = new(BMEdit);
        }

        int Tr1 = 0;
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
                            //BMEdit.SetPixel(segments[i][j][1], segments[i][j][0], Color.FromArgb(255, 255, 255));
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
            //HighlightGrid();
        }
        /*private void GetTerminals()
        {
            terminals = new();
            for (int i = 0; i < segments.Count; i++)
            {
                if (segments[i].Count > 0 && segments[i].Count >= Tr1)
                {
                    int[] temp = { segments[i][segments[i].Count - 1][0], segments[i][segments[i].Count - 1][1] };
                    terminals.Add(temp);
                }
            }
            editImg.Image = BMEdit;
        }*/

        /*private void HighlightGrid()
        {
            //sw.WriteLine("bfjsbv");
            List<List<int[]>> seg2 = new();
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\Book2.csv"))
            {
                sw.WriteLine(Convert.ToString(Tr1));
                for (int i = 0; i < segments.Count; i++)
                {

                    //sw.WriteLine(Convert.ToString(segments.Count) + "," + Convert.ToString(segments[i].Count) + "," + Convert.ToString(seg2.Count));
                    List<int[]> pixs = new();
                    int[] temp = { segments[i][0][0], segments[i][0][1] };
                    pixs.Add(temp);
                    for (int j = 1; j < segments[i].Count - 1; j++)
                    {
                        if (segments[i][j][0] % 5 != 1 && segments[i][j][1] % 5 != 1)
                        {
                            //BMEdit.SetPixel(segments[i][j][1], segments[i][j][0], Color.FromArgb(255, 255, 255));
                            BMEdit.SetPixel(segments[i][j][1], segments[i][j][0], BMOg3.GetPixel(segments[i][j][1], segments[i][j][0]));
                        }
                        else
                        {
                            int[] t = { segments[i][j][0], segments[i][j][1] };
                            pixs.Add(t);
                        }
                    }
                    int[] tl = { segments[i][segments[i].Count - 1][0], segments[i][segments[i].Count - 1][1] };
                    pixs.Add(tl);
                    seg2.Add(pixs);
                    sw.WriteLine(" ");
                    int x = seg2.Count - 1;
                    sw.WriteLine(Convert.ToString(seg2.Count));
                    for (int a = 0; a < seg2[x].Count; a++)
                    {
                        sw.WriteLine(Convert.ToString(seg2[x][a][0]) + "," + Convert.ToString(seg2[x][a][1]));
                    }
                }
                sw.WriteLine("");
                sw.WriteLine(Convert.ToString(seg2.Count));
                Graphics grp = Graphics.FromImage(BMEdit);
                for (int i = 0; i < seg2.Count; i++)
                {
                    Color clr = BMEdit.GetPixel(seg2[i][0][1], seg2[i][0][0]);
                    Pen cPen = new(clr);
                    sw.WriteLine(Convert.ToString(clr) + "," + Convert.ToString(cPen.Color));
                    for (int j = 1; j < seg2[i].Count; j++)
                    {
                        grp.DrawLine(cPen, seg2[i][j - 1][1], seg2[i][j - 1][0], seg2[i][j][1], seg2[i][j][0]);
                    }
                }
            }
            //editImg.Image = BMEdit;
            //Graphics grp = Graphics.FromImage(BMEdit);
            //sw.WriteLine("");
            //sw.WriteLine(Convert.ToString(seg2.Count));
            //sw.WriteLine("100");
            //Console.WriteLine("100");
            for (int a = 0; a < seg2.Count; a++)
            {
                sw.WriteLine(Convert.ToString(seg2[a].Count));
                if (seg2[a].Count < 1)
                    continue;
                Pen cPen = new(BMEdit.GetPixel(seg2[a][0][1], seg2[a][0][0]));
                for (int b = 1; b < seg2[a].Count; b++)
                {
                    grp.DrawLine(cPen, seg2[a][b - 1][1], seg2[a][b - 1][0], seg2[a][b][1], seg2[a][b][0]);
                }
            }
            editImg.Image = BMEdit;
        }*/

        List<List<int[]>> segments = new();
        List<int[]> terminals = new();
        List<int[]> noise = new();
        void cleanse(bool[,] visited, int i, int j, Color sclr)
        {
            if (i < 0 || j < 0 || i >= Dark1.GetLength(0) || j >= Dark1.GetLength(1) || Dark1[i, j] == 0 || visited[i, j])
                return;
            /*if (visited[i + 2, j + 2])
                return;*/
            visited[i, j] = true;
            BMEdit.SetPixel(j, i, sclr);
            int[] t = { i, j };
            noise.Add(t);
            cleanse(visited, i - 1, j - 1, sclr);
            cleanse(visited, i - 1, j, sclr);
            cleanse(visited, i - 1, j + 1, sclr);
            cleanse(visited, i, j - 1, sclr);
            cleanse(visited, i, j + 1, sclr);
            cleanse(visited, i + 1, j - 1, sclr);
            cleanse(visited, i + 1, j, sclr);
            cleanse(visited, i + 1, j + 1, sclr);
        }

        int flag = 0;

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
        int x = -1, y = -1;
        bool moving = false;
        Graphics Grp;
        private void editImg_MouseClick(object sender, MouseEventArgs e)
        {
            int i = e.Y, j = e.X;
            if (r != 0)
            {
                i = (int)(i / (1 + (double)(r) / 2));
                j = (int)(j / (1 + (double)(r) / 2));
            }
            BMEdit.SetPixel(j, i, Color.FromArgb(255, 255, 0));
            if (flag == 1)
            {
                noise = new();
                for (int x = i - 1; x <= i + 1; x++)
                {
                    for (int y = j - 1; y <= j + 1; y++)
                    {
                        erase(x, y);
                    }
                }
            }

            if (flag==2)
            {
                if (terminal.Count == 1)
                {
                    Grp.DrawLine(redPen, terminal[0][0], terminal[0][1], x, y);
                    terminal = new();
                }
                else
                {
                    int[] t = { x, y };
                    terminal.Add(t);
                }
            }

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
            if (flag == 4)
            {
                ColorBlack(i, j);
                //oG1.Image = BMEdit;
            }
            editImg.Image = BMEdit;
            org.Image = BMEdit;
        }
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
                BMEdit.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                moving = true;
            }
        }
        System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\Book2.csv");

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
                sw.WriteLine(Convert.ToString(x1) + "," + Convert.ToString(y1) + "," + Convert.ToString(x) + "," + Convert.ToString(y) + "," + Convert.ToString(BMEdit.GetPixel(x1, y1)) + "," + Convert.ToString(BMEdit.GetPixel(x, y)));
                /*BMEdit.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                BMEdit.SetPixel(x1, y1, Color.FromArgb(0, 0, 0));*/
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
            }
        }

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
        
        private void ColorBlack(int i, int j)
        {
            /*using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\Book2.csv"))
            {
                sw.WriteLine(Convert.ToString(i) + "," + Convert.ToString(j));
            }*/
            if (i<0 || j<0 || i>=h || j>=w)
            {
                return;
            }
            //sw.WriteLine(Convert.ToString(i) + "," + Convert.ToString(j) + "," + Convert.ToString(BMEdit.GetPixel(j, i)));
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
        List<int[]> ColorCounts = new();
        byte red = 100, green = 0, blue = 0;
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
                        int[] temp = { 10000 * red + 100 * green + blue, 0 };
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
        private void Fill(int i, int j, Color fclr)
        {
            if (i<0 || j<0 || i>=h || j>=w || vis[i,j])
            {
                return;
            }
            vis[i, j] = true;
            BMEdit.SetPixel(j, i, fclr);
            ColorCounts.Last()[1]++;
            Fill(i - 1, j, fclr);
            Fill(i + 1, j, fclr);
            Fill(i, j - 1, fclr);
            Fill(i, j + 1, fclr);
        }
    }
}
