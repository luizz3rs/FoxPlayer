using FoxPlayerClassic.Service;
using System;
using System.Windows.Forms;

namespace FoxPlayerClassic
{
    public partial class Form1 : Form
    {
        #region .: Variables :.

        private PlayerService _service;

        #endregion


        #region .: Constructor :.

        public Form1()
        {
            InitializeComponent();

            LoadDefaultValues();

            InstantiateObjects();
        }

        #endregion

        #region .: Auxiliar Methods :.

        private void LoadDefaultValues()
        {
            trackVolume.Value = 50;
            lblVolume.Text = "50%";
        }

        private void InstantiateObjects()
        {
            _service = new PlayerService(player: ref player);
        }

        #endregion

        #region .: Events :.

        private void trackList_SelectedIndexChanged(object sender, EventArgs e) => _service.ChangeTracklistSong(trackList: ref trackList,
                                                                                                                picArt: ref picArt);

        private void btnStop_Click(object sender, EventArgs e) => _service.StopPlayer(progressPlayer: ref progressBar1);

        private void btnPause_Click(object sender, EventArgs e) => _service.PausePlayer();

        private void btnPlay_Click(object sender, EventArgs e) => _service.StarPlayer();

        private void btnNext_Click(object sender, EventArgs e) => _service.SkipSong(trackList: ref trackList);

        private void btnPrevious_Click(object sender, EventArgs e) => _service.PreviousSong(trackList: ref trackList);

        private void timer1_Tick(object sender, EventArgs e) => _service.TimerTick(progressPlayer: ref progressBar1,
                                                                                   lblTrStart: ref lblTrStart,
                                                                                   lblTrEnd: ref lblTrEnd);

        private void trackVolume_Scroll(object sender, EventArgs e) => _service.ChangeTrackListVolume(lblVolume: ref lblVolume,
                                                                                                      trackVolume: ref trackVolume);

        private void progressBar1_MouseDown(object sender, MouseEventArgs e) => _service.DoProgressBarMouseDownEvent(e: ref e,
                                                                                                                     progressPlayer: ref progressBar1);

        private void btnOpen_Click(object sender, EventArgs e) => _service.SearchFile(trackList: ref trackList);

        #endregion
    }
}
