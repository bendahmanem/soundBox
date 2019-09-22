using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NAudio.Lame;
using NAudio.Wave;

namespace SoundBox
{
    public partial class Form1 : Form
    {
        private const string SoundLocation = @"C:\Users\Mounir\source\repos\SoundBox\SoundBox\Sounds\Aoe2\blacksmith.wav";
        private const string BlacksmithFilename = "blacksmith.wav";
        private WaveFileWriter RecordedAudioWriter = null;
        private WasapiLoopbackCapture CaptureInstance = null;
        private WaveOut waveOut = null;
        string outputFilePath = @"C:\Users\";
        string inputFileWav = "";
        string outputFilePathMP3 = @"C:\Users\";
        string lectureFilePath = @"";
        private Mp3FileReader reader = null;
        static System.Windows.Forms.Timer s_myTimer = new System.Windows.Forms.Timer();
        // Resources
        /*        Stream str = Properties.Resources.mySoundFile;
                RecordPlayer rp = new RecordPlayer();
                rp.Open(new WaveReader(str));
          rp.Play();*/


        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            button12.Enabled = false;
            button13.Enabled = false;
            button14.Enabled = false;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.donkey);
            player.Play();
        }


        private void Button6_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.wonder);
            player.Play();
        }


        private void Button3_Click(object sender, EventArgs e)
        {
                
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.blacksmith);
            player.Play();
        }

        // Stop and save recording
        private void Button12_Click(object sender, EventArgs e)
        {
            this.CaptureInstance.RecordingStopped += (s, a) =>
            {
                this.RecordedAudioWriter.Dispose();
                this.RecordedAudioWriter = null;
                CaptureInstance.Dispose();
            };

            this.CaptureInstance.StopRecording();
            this.BackgroundImage = null;
            button11.Enabled = true;
            button12.Enabled = false;

        }

        // Start recordiing
        private void Button11_Click(object sender, EventArgs e)
        {
           
            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.InitialDirectory = "C:\\";
            savefiledialog.RestoreDirectory = true;
            savefiledialog.Title = "Choisissez un titre";
            savefiledialog.DefaultExt = ".wav";
            savefiledialog.Filter = "Wav Files (*.wav)|*wav";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                this.outputFilePath = savefiledialog.FileName;
            }
            // savefiledialog.FileOk += new System.ComponentModel.CancelEventHandler(SaveFileDialog1_FileOk);
            this.CaptureInstance = new WasapiLoopbackCapture();

            // Redefine the audio writer instance with the given configuration
            this.RecordedAudioWriter = new WaveFileWriter(outputFilePath, CaptureInstance.WaveFormat);

            this.CaptureInstance.DataAvailable += (s, a) =>
            {
                this.RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };

            this.CaptureInstance.StartRecording();
            button11.Enabled = false;
            button12.Enabled = true;
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(SoundBox.Resources.blacksmith);
                    player.Play();
                    break;
                case Keys.Z:
                    player = new System.Media.SoundPlayer(SoundBox.Resources.donkey);
                    player.Play();
                    break;
                case Keys.E:
                    player = new System.Media.SoundPlayer(SoundBox.Resources.wonder);
                    player.Play();
                    break;
                case Keys.Q:
                    player = new System.Media.SoundPlayer(SoundBox.Resources.attackwarning);
                    player.Play();
                    break;
                case Keys.S:
                    player = new System.Media.SoundPlayer(SoundBox.Resources.victory);
                    player.Play();
                    break;
                case Keys.D:
                    player = new System.Media.SoundPlayer(SoundBox.Resources.monastery);
                    player.Play();
                    break;
                case Keys.W:
                    player = new System.Media.SoundPlayer(SoundBox.Resources.barracks);
                    player.Play();
                    break;
                case Keys.X:
                    player = new System.Media.SoundPlayer(SoundBox.Resources.camelselect1);
                    player.Play();
                    break;
                case Keys.C:
                    player = new System.Media.SoundPlayer(SoundBox.Resources.castle);
                    player.Play();
                    break;

            }

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.attackwarning);
            player.Play();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.monastery);
            player.Play();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.barracks);
            player.Play();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.camelselect1);
            player.Play();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.castle);
            player.Play();
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.attackwarning);
            player.Play();
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.victory);
            player.Play();
        }


        // Play a wav file
        private void Button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.InitialDirectory = "C:\\";
            openfiledialog.RestoreDirectory = true;
            openfiledialog.Title = "Choisissez un fichier";
            openfiledialog.DefaultExt = ".mp3";
            openfiledialog.Filter = "Mp3 Files (*.mp3)|*mp3";
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                lectureFilePath = openfiledialog.FileName;
                waveOut = new WaveOut();
                var reader = new Mp3FileReader(lectureFilePath);
            }
            reader = new Mp3FileReader(lectureFilePath);
            button13.Enabled = true;
            button14.Enabled = true;
        }


        // PAUSE 
        private void Button13_Click(object sender, EventArgs e)
        {
            waveOut.Pause();
        }


        // STOP 
        private void Button14_Click(object sender, EventArgs e)
        {
            button13.Enabled = false;
            button14.Enabled = false;
        }

        // RE
        private void Button15_Click(object sender, EventArgs e)
        {

        }


        // COnvert to mp3
        private void Button16_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefiledialog1 = new SaveFileDialog();
            savefiledialog1.InitialDirectory = "C:\\";
            savefiledialog1.RestoreDirectory = true;
            savefiledialog1.Title = "Choisissez un titre";
            savefiledialog1.DefaultExt = ".wav";
            savefiledialog1.Filter = "Wav Files (*.wav)|*wav";

            if (savefiledialog1.ShowDialog() == DialogResult.OK)
            {
                inputFileWav = savefiledialog1.FileName;
            }
            ConvertWavToMp3(inputFileWav, outputFilePath);
        }

        public void ConvertWavToMp3(string WavFile, string outPutFile)
        {

            WaveFileReader rdr = new WaveFileReader(WavFile);
            using (var wtr = new NAudio.Lame.LameMP3FileWriter(outPutFile, rdr.WaveFormat, 128))
            {
                rdr.CopyTo(wtr);
                rdr.Dispose();
                wtr.Dispose();
                return;
            }
        }
    }
}
