﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FoxPlayerClassic
{
    public partial class Form1 : Form
    {
        public Form1()

        {
            InitializeComponent();
            trackVolume.Value = 50;
            lblVolume.Text = "50%";
        }
        string[] paths, files;

        private void trackList_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.URL = paths[trackList.SelectedIndex];
            player.Ctlcontrols.play();
            
            try
            {
                var file = TagLib.File.Create(paths[trackList.SelectedIndex]);
                var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                picArt.Image = Image.FromStream(new MemoryStream(bin));
            }
            catch
            {

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
            progressBar1.Value = 0;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.play();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(trackList.SelectedIndex < trackList.Items.Count-1)
            {
                trackList.SelectedIndex = trackList.SelectedIndex + 1;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if(trackList.SelectedIndex > 0)
            {
                trackList.SelectedIndex = trackList.SelectedIndex - 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(player.playState==WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar1.Maximum = (int)player.Ctlcontrols.currentItem.duration;
                progressBar1.Value = (int)player.Ctlcontrols.currentPosition;
            }
            try
            {
                lblTrStart.Text = player.Ctlcontrols.currentPositionString;
                lblTrEnd.Text = player.Ctlcontrols.currentItem.durationString.ToString();
            }
            catch
            {

            }
        }

        private void trackVolume_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = trackVolume.Value;
            lblVolume.Text = trackVolume.Value.ToString() + "%";
        }

        private void progressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            player.Ctlcontrols.currentPosition = player.currentMedia.duration * e.X / progressBar1.Width;   
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if(ofd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                files = ofd.FileNames;
                paths = ofd.FileNames;
                for(int x = 0; x < files.Length;x++ )
                {
                    trackList.Items.Add(files[x]);
                }
            }
        }
    }
}
