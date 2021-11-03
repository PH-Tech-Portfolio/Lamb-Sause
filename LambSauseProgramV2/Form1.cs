using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//add this so I can make unhappy windows sounds
using System.Media;
//create timers in code so I don't have to do any form stuff
using System.Timers;
//NuGet package for tampering with windows audio
using AudioSwitcher;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;

namespace LambSauseProgramV2
{
    public partial class Form1 : Form
    {
        //define user controls
        Form testnew;
        //intelisense made these for me
        private int _ScreenWidth;
        private int _ScreenHeight;
        Random rand;
        Point mypoint;
        SoundPlayer musicbox;
        private static System.Timers.Timer aTimer;
        private static System.Timers.Timer aTimer2;

        public Form1()
        {
            InitializeComponent();
            musicbox = new SoundPlayer(LambSauseProgramV2.Properties.Resources.laumb_sausse_);

            musicbox.Play();
            //can I hide my form??? (neither of these work and I am not sure why! (NOTE: ask Derek)
            this.Hide();
            this.Visible = false;

            //timers (some code from MS Online Docs)
            aTimer = new System.Timers.Timer(2100);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            //timers (some code from MS Online Docs)
            aTimer2 = new System.Timers.Timer(25);
            // Hook up the Elapsed event for the timer. 
            aTimer2.Elapsed += ATimer2_Elapsed;
            aTimer2.AutoReset = true;
            aTimer2.Enabled = true;

            //instanciate and assign some stuff
            rand = new Random();

        }

        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //set windows voliume to max (like 95)
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume = 95;
            //play the sound
            musicbox.Play();
        }

        private void ATimer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            //instanciat the new form
            testnew = new Form();

            //put at begining to make sure it is executed
            testnew.ShowInTaskbar = false;

            //grabbing these for the constaints of the screen. This way it is better than guissing wher to place boxes
            _ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            _ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //System.Windows.Forms.Screen.AllScreens[1];
            //figure out how to use        ^^^ to put them on both screens

            //call this here becasue I want it to change every time
            mypoint.X = rand.Next(_ScreenWidth);
            mypoint.Y = rand.Next(_ScreenHeight);

            //took this stuff from example in stack overflow (weirdly I remember previous try to work)
            //https://stackoverflow.com/questions/24016638/set-form-location-c-sharp/24016834
            testnew.StartPosition = FormStartPosition.Manual;
            testnew.Left = mypoint.X;
            testnew.Top = mypoint.Y;

            //change the properties of the new form (we want it as inconveinent as possible)
            testnew.FormBorderStyle = FormBorderStyle.None;
            testnew.Width = 150;
            testnew.Height = 80;

            //change its image
            testnew.BackgroundImage = LambSauseProgramV2.Properties.Resources.gordonRamsayM;
            //testnew.BackgroundImageLayout = ImageLayout.Center;

            testnew.Show();
        }
    }
}
