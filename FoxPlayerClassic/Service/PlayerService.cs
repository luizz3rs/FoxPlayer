using AxWMPLib;
using FoxPlayerClassic.Enum;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FoxPlayerClassic.Service
{
    public class PlayerService
    {
        #region ..: Variables :.

        private readonly AxWindowsMediaPlayer _player;
        private string[] _file;
        private string[] _path;

        #endregion

        #region .: Constructor :.
        public PlayerService(ref AxWindowsMediaPlayer player)
        {
            _player = player;
        }

        #endregion

        #region .: Methods :.

        public void StopPlayer(ref ProgressBar progressPlayer)
        {
            _player.Ctlcontrols.stop();
            progressPlayer.Value = 0;
        }

        public void PausePlayer()
        {
            _player.Ctlcontrols.pause();
        }

        public void StarPlayer()
        {
            _player.Ctlcontrols.play();
        }

        public void SkipSong(ref ListBox trackList)
        {
            if (trackList.SelectedIndex < trackList.Items.Count - 1)
                trackList.SelectedIndex++;
        }

        public void PreviousSong(ref ListBox trackList)
        {
            if (trackList.SelectedIndex > 0)
                trackList.SelectedIndex--;
        }

        public void TimerTick(ref ProgressBar progressPlayer, ref Label lblTrStart, ref Label lblTrEnd)
        {
            try
            {
                if (_player.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    progressPlayer.Maximum = (int)_player.Ctlcontrols.currentItem.duration;
                    progressPlayer.Value = (int)_player.Ctlcontrols.currentPosition;
                }

                lblTrStart.Text = _player.Ctlcontrols.currentPositionString;
                lblTrEnd.Text = _player.Ctlcontrols.currentItem.durationString.ToString();
            }
            catch (Exception ex)
            {
                //# Wrtite to a log file to prevent pop up error msgs
            }
        }

        public void ChangeTracklistSong(ref ListBox trackList, ref PictureBox picArt)
        {
            try
            {
                _player.URL = _path[trackList.SelectedIndex];
                _player.Ctlcontrols.play();

                var file = TagLib.File.Create(_path[trackList.SelectedIndex]);
                var bin = file.Tag.Pictures[(int)PictureEnum.DefaultPicture].Data.Data;
                picArt.Image = Image.FromStream(new MemoryStream(bin));
            }
            catch (Exception ex)
            {
                //# Wrtite to a log file to prevent pop up error msgs
            }
        }

        public void ChangeTrackListVolume(ref Label lblVolume, ref TrackBar trackVolume)
        {
            _player.settings.volume = trackVolume.Value;
            lblVolume.Text = $"{trackVolume.Value}%";
        }

        public void DoProgressBarMouseDownEvent(ref MouseEventArgs e, ref ProgressBar progressPlayer)
        {
            //# Fix to Null Reference Exception on ProgressBar Click
            if (_player.currentMedia == null)
                return;

            _player.Ctlcontrols.currentPosition =
                (_player.currentMedia.duration * e.X) / (progressPlayer.Width);
        }

        public void SearchFile(ref ListBox trackList)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Videos (*.mp4, *mov, *.wmv) | *.mp4;*.mov;*.wmv | Songs (*.mp3) | *.mp3;"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _file = ofd.FileNames;
                _path = ofd.FileNames;

                for (int x = 0; x < _file.Length; x++)
                    trackList.Items.Add(_path[x]);
            }
        }

        #endregion
    }
}
