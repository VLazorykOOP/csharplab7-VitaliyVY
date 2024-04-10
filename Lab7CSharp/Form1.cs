using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private int currentIndex = 0;
        private readonly Color[] colors = { Color.Red, Color.Yellow, Color.Green };
        private int interval = 3000; // інтервал зміни кольорів у мілісекундах


        public Form1()
        {
            InitializeComponent();
            InitializeTimer();

        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = interval;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            switch (currentIndex)
            {
                case 0:
                    pictureBox1.BackColor = Color.Red;
                    pictureBox2.BackColor = Color.Black; // Вимикаємо інші піктограми
                    pictureBox3.BackColor = Color.Black;
                    break;
                case 1:
                    pictureBox1.BackColor = Color.Black;
                    pictureBox2.BackColor = Color.Yellow;
                    pictureBox3.BackColor = Color.Black;
                    break;
                case 2:
                    pictureBox1.BackColor = Color.Black;
                    pictureBox2.BackColor = Color.Black;
                    pictureBox3.BackColor = Color.Green;
                    break;
            }

            // Збільшення індексу для наступного кольору
            currentIndex = (currentIndex + 1) % 3; // 3 - кількість піктограм
        }


        private void Start_Click(object sender, EventArgs e)
        {
            // Отримання значення кількості секунд від користувача
            if (int.TryParse(textBox1.Text, out int seconds))
            {
                if (seconds == 0)
                {
                    MessageBox.Show("Інтервал не може бути рівним нулю. Будь ласка, введіть коректне значення.");
                    return; // Повертаємося, не встановлюючи таймер
                }

                // Встановлення інтервалу таймера
                interval = seconds * 1000; // переведення секунд в мілісекунди
                timer.Interval = interval;
                timer.Start();
            }
            else
            {
                MessageBox.Show("Невірний формат введених секунд. Будь ласка, введіть ціле число.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        

        private Bitmap ExtractComponent(Bitmap originalImage, ColorComponent component)
        {
            Bitmap extractedImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    Color pixel = originalImage.GetPixel(x, y);
                    Color newPixel = Color.Black;

                    switch (component)
                    {
                        case ColorComponent.Red:
                            newPixel = Color.FromArgb(pixel.R, 0, 0);
                            break;
                        case ColorComponent.Green:
                            newPixel = Color.FromArgb(0, pixel.G, 0);
                            break;
                        case ColorComponent.Blue:
                            newPixel = Color.FromArgb(0, 0, pixel.B);
                            break;
                    }

                    extractedImage.SetPixel(x, y, newPixel);
                }
            }

            return extractedImage;
        }

        private void browseButton_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image
                Bitmap image = new Bitmap(openFileDialog.FileName);
                originalPictureBox.Image = image;
            }
        }

        private void extractButton_Click_1(object sender, EventArgs e)
        {
            if (originalPictureBox.Image != null)
            {
                Bitmap originalImage = new Bitmap(originalPictureBox.Image);

                // Extract color components based on selected radio button
                Bitmap extractedImage = null;
                if (redRadioButton.Checked)
                {
                    extractedImage = ExtractComponent(originalImage, ColorComponent.Red);
                }
                else if (greenRadioButton.Checked)
                {
                    extractedImage = ExtractComponent(originalImage, ColorComponent.Green);
                }
                else if (blueRadioButton.Checked)
                {
                    extractedImage = ExtractComponent(originalImage, ColorComponent.Blue);
                }

                if (extractedImage != null)
                {
                    extractedPictureBox.Image = extractedImage;
                }
            }
            else
            {
                MessageBox.Show("Please select an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public enum ColorComponent
    {
        Red,
        Green,
        Blue
    
}
}
