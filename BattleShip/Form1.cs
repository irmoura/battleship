using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class Form1 : Form
    {

        private Graphics graphics = null;
        private int centralHorizontalLine;
        private int centralVerticalLine;
        private string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };
        //
        int size = 60;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            centralHorizontalLine = (this.Width / 2) - 8;
            centralVerticalLine = (this.Height / 2) - 7;
            //
            graphics = this.CreateGraphics();
            //
            //DrawHorizontalLine(Color.Red, 0, this.Width, centralVerticalLine, 2);//X
            //DrawVerticalLine(Color.Red, centralHorizontalLine, 0, this.Height, 2);//Y
            //
            //DrawVerticalLine(Color.Red, 30, 0, this.Height, 2);//Y
            //DrawHorizontalLine(Color.Red, 0, this.Width, 30, 2);//X
            //
            //DrawVerticalLine(Color.Red, 90, 0, this.Height, 2);//Y
            //DrawHorizontalLine(Color.Red, 0, this.Width, 930, 2);//X
            //
            int count = 0;
            int countL = 0;
            int countX = 0;
            int countY = 0;
            for (int i = 0; i < 16; i++)//LINHAS
            {
                countX = 0;
                for (int j = 0; j < 16; j++)//COLUNAS
                {
                    DrawRectangle(Color.White, countX, countY, size, size);
                    if (countX == 0 && count < 15)
                    {
                        DrawString(Color.White, (countX + (count < 9 ? 19 : 12)), (countY + 16), $"{(count + 1)}");
                    }
                    if (countX > 0 && count == 15)
                    {
                        DrawString(Color.White, (countX + 18), (countY + 16), $"{letras[countL]}");
                        countL++;
                    }
                    countX += size;
                }
                countY += size;
                count++;
            }
            //
            count = 0;
            countL = 0;
            countX = 960;
            countY = 0;
            for (int i = 0; i < 16; i++)//LINHAS
            {
                countX = 960;
                for (int j = 0; j < 16; j++)//COLUNAS
                {
                    DrawRectangle(Color.White, countX, countY, size, size);
                    if (countX == 960 && count < 15)
                    {
                        DrawString(Color.White, (countX + (count < 9 ? 19 : 12)), (countY + 16), $"{(count + 1)}");
                    }
                    if (countX > 960 && count == 15)
                    {
                        DrawString(Color.White, (countX + 18), (countY + 16), $"{letras[countL]}");
                        countL++;
                    }
                    countX += size;
                }
                countY += size;
                count++;
            }
            //1 PORTA-AVIÕES
            PortaAvioes(Color.Yellow, "F2");
            //2 ENCOURAÇADOS
            Encouracado(Color.Red, "I6");
            Encouracado(Color.Red, "K12");
            //3 CRUZADORES
            Cruzador(Color.Orange, "L2");
            Cruzador(Color.Orange, "H8");
            Cruzador(Color.Orange, "B13");
            //3 HIDROAVIÕES
            HidroAviao(Color.Blue, "E5");
            HidroAviao(Color.Blue, "C9");
            HidroAviao(Color.Blue, "G14");
            //4 SUBMARINOS
            Submarino(Color.Green, "B3");
            Submarino(Color.Green, "F7");
            Submarino(Color.Green, "L9");
            Submarino(Color.Green, "F11");
        }

        private Point Decodificador(string localizacao)
        {
            int xPosition = Array.IndexOf(letras, localizacao.Substring(0, 1));
            xPosition++;
            xPosition = xPosition * 60;
            //
            string numeroS = localizacao.Length == 2 ? localizacao.Substring(1, 1) : localizacao.Substring(1, 2);
            int yPosition = Convert.ToInt32(numeroS);
            yPosition--;
            yPosition = yPosition * 60;
            return new Point(xPosition, yPosition);
        }

        private void HidroAviao(Color color, string localizacao)
        {
            Point point = Decodificador(localizacao);
            FillRectangle(Color.Blue, point.X, point.Y, size, size);
            FillRectangle(Color.Blue, point.X + size, point.Y - size, size, size);
            FillRectangle(Color.Blue, point.X + (size * 2), point.Y, size, size);
        }

        private void Submarino(Color color, string localizacao)
        {
            Point point = Decodificador(localizacao);
            FillRectangle(color, point.X, point.Y, size, size);
        }

        private void Encouracado(Color color, string localizacao)
        {
            Point point = Decodificador(localizacao);
            for (int i = 0; i < 4; i++)
            {
                FillRectangle(color, point.X, point.Y, size, size);
                point.X += size;
            }
        }

        private void PortaAvioes(Color color, string localizacao)
        {
            Point point = Decodificador(localizacao);
            for (int i = 0; i < 5; i++)
            {
                FillRectangle(color, point.X, point.Y, size, size);
                point.X += size;
            }
        }

        private void Cruzador(Color color, string localizacao)
        {
            Point point = Decodificador(localizacao);
            for (int i = 0; i < 2; i++)
            {
                FillRectangle(color, point.X, point.Y, size, size);
                point.X += size;
            }
        }

        private void DrawHorizontalLine(Color color, int x1, int x2, int y, int size = 1)
        {
            graphics.DrawLine(new Pen(new SolidBrush(color), size), x1, y, x2, y);
        }

        private void DrawVerticalLine(Color color, int x, int y1, int y2, int size = 1)
        {
            graphics.DrawLine(new Pen(new SolidBrush(color), size), x, y1, x, y2);
        }

        private void DrawRectangle(Color color, int x, int y, int width, int height, int size = 2)
        {
            Rectangle rectangle = new Rectangle(x, y, width, height);
            graphics.DrawRectangle(new Pen(new SolidBrush(color), size), rectangle);
        }

        private void FillRectangle(Color color, int x, int y, int width, int height)
        {
            Rectangle rectangle = new Rectangle(x, y, width, height);
            graphics.FillRectangle(new SolidBrush(color), rectangle);
        }

        private void DrawString(Color color, int x, int y, string s)
        {
            Font font = new Font("Helvetica Narrow", 18, FontStyle.Regular);
            Brush brush = new SolidBrush(color);
            graphics.DrawString(s, font, brush, x, y);
        }

        private void DrawCross(Color color, int x, int y, int size)
        {
            graphics.DrawLine(new Pen(new SolidBrush(color), 5), x, y, (x + size), (y + size));
            graphics.DrawLine(new Pen(new SolidBrush(color), 5), x, y + size, x + size, y);
        }

        private void Explosion(int situation)
        {
            string caminhoDoArquivoMP3 = situation == 0 ? @"C:\Users\corag\Downloads\water-bomb-exploding.wav" : @"C:\Users\corag\Downloads\bomb-explosion.wav";

            try
            {
                // Crie uma instância da classe SoundPlayer e carregue o arquivo MP3.
                using (SoundPlayer player = new SoundPlayer(caminhoDoArquivoMP3))
                {
                    // Reproduza o som.
                    player.PlaySync(); // PlaySync() bloqueará a execução do código até que o som termine.
                    //player.Play(); // Use Play() se quiser que o som seja reproduzido em segundo plano.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao reproduzir o arquivo: " + ex.Message);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            if (point.X >= centralHorizontalLine + 60 && point.Y <= 900)
            {
                //this.Text = $"{point.X} | {point.Y}";
                bool found = false;
                for (int i = size; i < 960; i += size)
                {
                    if (point.Y <= (i - 4))
                    {
                        for (int j = 1080; j < 1980; j += size)
                        {
                            if (point.X <= (j - 4))
                            {
                                Random random = new Random();
                                int situation = random.Next(0, 2);
                                if (situation == 0)
                                {
                                    //DrawRectangle(Color.Lime, (j - size), (i - size), size, size);
                                    DrawCross(Color.Red, (j - size), (i - size), size);
                                    Explosion(situation);
                                }
                                else
                                {
                                    //DrawRectangle(Color.Lime, (j - size), (i - size), size, size);
                                    DrawCross(Color.Green, (j - size), (i - size), size);
                                    Explosion(situation);
                                }
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
            }
            else
            {
                //this.Text = $"{point.X} | {point.Y}";
            }
        }
    }
}
