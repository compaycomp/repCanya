using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Cursach
{
    public partial class CatchAButton : Form
    {
        private TimeSpan time;
        private Random rnd = new Random();
        private int clickCount = 0;
        private TimeSpan gameTime;

        public CatchAButton(string login)
        {
            InitializeComponent();

            accPlace.Text = login;
            accPlace.Font = new Font("Unispace", 12, FontStyle.Bold);

            time = TimeSpan.Zero;
            timeLabel.Text = time.ToString(@"mm\:ss");
            gameTimer.Interval = 1000;

            timerButt.Tick += TimerButt_Tick;

            mainButton.Size = new Size(40, 40);
            mainButton.BackColor = Color.Red;
            mainButton.FlatAppearance.BorderSize = 0;
            mainButton.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#bc0c1a");
            mainButton.FlatAppearance.MouseOverBackColor = Color.Red;
            mainButton.FlatStyle = FlatStyle.Flat;

            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, mainButton.Width, mainButton.Height);
            mainButton.Region = new Region(graphicsPath);

            mainButton.Click += MainButton_Click;

            SetRandomButtonLocation();

            resetButt.Visible = false;

            mainButton.Enabled = false;

            startButton.Enabled = false;

            easyButton.Click += EasyButton_Click;
            mediumButton.Click += MediumButton_Click;
            hardButton.Click += HardButton_Click;

            diffLevelCheck.Text = "УРОВЕНЬ НЕ ВЫБРАН";
            diffLevelCheck.Font = new Font("Unispace", 12, FontStyle.Bold);

            this.KeyPreview = true;
        }

        void SetRandomButtonLocation()
        {
            int maxX = panel1.Width - mainButton.Width;
            int maxY = panel1.Height - mainButton.Height;

            int newX = rnd.Next(0, maxX);
            int newY = rnd.Next(0, maxY);

            mainButton.Location = new Point(newX, newY);
            mainButton.Enabled = true; // Включаем кнопку после перемещения
        }

        private void TimerButt_Tick(object sender, EventArgs e)
        {
            SetRandomButtonLocation();
        }

        private void SetButtonInterval(float intervalInSeconds)
        {
            timerButt.Interval = (int)(1000 * intervalInSeconds);
        }


        private void MainButton_Click(object sender, EventArgs e)
        {
            clickCount++;
            label1.Text = clickCount.ToString();
            MessageBox.Show("Количество нажатий равно: " + clickCount);
            if (clickCount == 3)
            {
                // Останавливаем кнопку
                timerButt.Stop();

                // Останавливает таймер игры
                gameTimer.Stop();

                // Выводим сообщение с результатом
                MessageBox.Show("Ваш результат: " + time.ToString(@"mm\:ss"));

                //UpdateResults(gameTime);

                // Очищает таймер
                time = TimeSpan.Zero;
                timeLabel.Text = time.ToString(@"mm\:ss");

                resetButt.Visible = false;
                startButton.Enabled = true;
                easyButton.Enabled = true;
                mediumButton.Enabled = true;
                hardButton.Enabled = true;

                clickCount = 0;
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            gameTimer.Start();
            timerButt.Start();
            mainButton.Enabled = true;
            easyButton.Enabled = false;
            mediumButton.Enabled = false;
            hardButton.Enabled = false;
            startButton.Enabled = false;
            resetButt.Visible = true;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            time = time.Add(TimeSpan.FromSeconds(1));

            if (time.TotalMinutes >= 10)
            {
                gameTimer.Stop();
                MessageBox.Show("Время вышло!");
                resetButt.Visible = false;
            }

            timeLabel.Text = time.ToString(@"mm\:ss");
        }

        private void EasyButton_Click(object sender, EventArgs e)
        {
            SetButtonInterval(1f); //интервал 1 секунда
            startButton.Enabled = true;
            diffLevelCheck.Text = "ВЫБРАН УРОВЕНЬ 1";
        }

        private void MediumButton_Click(object sender, EventArgs e)
        {
            SetButtonInterval(0.8f); //интервал 0.8 секунды
            startButton.Enabled = true;
            diffLevelCheck.Text = "ВЫБРАН УРОВЕНЬ 2";
        }

        private void HardButton_Click(object sender, EventArgs e)
        {
            SetButtonInterval(0.6f); //интервал 0.6 секунды
            startButton.Enabled = true;
            diffLevelCheck.Text = "ВЫБРАН УРОВЕНЬ 3";
        }

        private void StartGame()
        {
            clickCount = 0;
            time = gameTime;
            timeLabel.Text = time.ToString(@"mm\:ss");
            timerButt.Start();
            mainButton.Enabled = true;
            resetButt.Enabled = false;
            resetButt.Visible = false;
            startButton.Enabled = false;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.White;
        }

        private void RollUp(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void RollUpButton_MouseEnter(object sender, EventArgs e)
        {
            rollUpButton.ForeColor = Color.Green;
        }

        private void RollUpButton_MouseLeave(object sender, EventArgs e)
        {
            rollUpButton.ForeColor = Color.White;
        }

        Point lastPoint;
        private void Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void ResetButt_Click(object sender, EventArgs e)
        {
            timerButt.Stop();
            gameTimer.Stop();
            easyButton.Enabled = true;
            mediumButton.Enabled = true;
            hardButton.Enabled = true;
            mainButton.Enabled = false;
            startButton.Enabled = true;
            clickCount = 0;
            time = TimeSpan.Zero;
            timeLabel.Text = time.ToString(@"mm\:ss");
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }
    }
}
