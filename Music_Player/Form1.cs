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

namespace Music_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            track_volume.Value = 50;
            lbl_volume.Text = track_volume.Value.ToString() + "%";
        }

        string[] paths, files;

        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.URL = paths[track_list.SelectedIndex];

            player.Ctlcontrols.play();
            try
            {
                var file = TagLib.File.Create(paths[track_list.SelectedIndex]);
                var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                pic_art.Image = Image.FromStream(new MemoryStream(bin));
            }
            catch
            {
                //pic_art.Image = null;
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
            p_bar.Value = 0;
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.play();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex < track_list.Items.Count - 1)

            {

                track_list.SelectedIndex = track_list.SelectedIndex + 1;

            }
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex > 0)

            {

                track_list.SelectedIndex = track_list.SelectedIndex - 1;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)

                {
                    p_bar.Maximum = (int)player.Ctlcontrols.currentItem.duration;

                    p_bar.Value = (int)player.Ctlcontrols.currentPosition;
                    lbl_track_start.Text = player.Ctlcontrols.currentPositionString;
                }
                //lbl_track_start.Text = player.Ctlcontrols.currentPositionString;
                lbl_track_end.Text = player.Ctlcontrols.currentItem.durationString.ToString();
                if (player.URL != String.Empty)
                {
                    if (track_list.SelectedIndex < track_list.Items.Count && lbl_track_start.Text == lbl_track_end.Text)
                    {
                        track_list.SelectedIndex = track_list.SelectedIndex + 1;
                    }
                }
            }
            catch
            {

            }
            
            
        }

        private void track_volume_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = track_volume.Value;

            lbl_volume.Text = track_volume.Value.ToString() + "%";
        }

        private void p_bar_MouseDown(object sender, MouseEventArgs e)
        {
            player.Ctlcontrols.currentPosition = player.currentMedia.duration * e.X / p_bar.Width;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void player_Enter(object sender, EventArgs e)
        {

        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)

            {

                files = ofd.SafeFileNames;

                paths = ofd.FileNames;

                for (int x = 0; x < files.Length; x++)

                {

                    track_list.Items.Add(files[x]);

                }

            }
        }
    }
}
