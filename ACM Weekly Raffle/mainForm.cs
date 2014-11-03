using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace ACM_Weekly_Raffle
{
    public partial class mainForm : Form
    {
        private Winners winners;
        private SpeechSynthesizer speaker = new SpeechSynthesizer();
        public mainForm()
        {
            InitializeComponent();

            this.speaker.SelectVoice(this.speaker.GetInstalledVoices()[2].VoiceInfo.Name);
            winners = new Winners();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string winner = winners.selectWinner();
            if(winner == null)
            {
                MessageBox.Show("No winners remain!  Resetting winner list!");
                winners.reset();
                return;
            }
            string[] nameParts = winner.Split(' ', '-');
            this.firstNameLabel.Text = nameParts[0];
            if (nameParts.Length > 2)
            {
                string pictureURL = "http://graph.facebook.com/" + nameParts[2] + "/picture?width=100&height=100";
                this.userPictureBox.Load(pictureURL);
            }
            else
            {
                this.userPictureBox.Image = new Bitmap(100, 100);
            }
            if (nameParts.Length > 1)
            {
                this.lastNameLabel.Text = nameParts[1];
                this.speaker.SpeakAsync(nameParts[0] + " " + nameParts[1] + ". All hail the consh!");
            }
            else
            {
                this.lastNameLabel.Text = "";
                this.speaker.SpeakAsync(nameParts[0] + ". All hail the consh!");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            winners.Save();
        }

        private void addWinnerButton_Click(object sender, EventArgs e)
        {
            winners.AddWinner();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            winners.reset();
        }
    }
}
