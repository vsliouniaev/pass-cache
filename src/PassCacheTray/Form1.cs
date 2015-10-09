using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using PassCacheTray.Models;
using PassCacheTray.Properties;
using PassCacheTray.Util;

namespace PassCacheTray
{
    public partial class frmMain : Form
    {
        private PassCacheTraySetting Setting { get; set; }
        private Worker Worker { get; set; }
        private GlobalHotkey GHK { get; set; }

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GHK != null)
            {
                GHK.Unregister();
            }

            Keys k;
            if (Keys.TryParse(tbxLetter.Text, true, out k))
            {
                Setting.ComboLetter = k;
            }
            Setting.ComboModifier1 = (HotKeyModifiers) cbxModifier1.SelectedValue;
            Setting.ComboModifier2 = (HotKeyModifiers) cbxModifier2.SelectedValue;
            Setting.PassCacheUrl = tbxServerUri.Text;
            Worker.ServerUri = Setting.PassCacheUrl;

            Setting.SaveToSettings(Settings.Default);
            
            GHK = new GlobalHotkey(Setting.ComboModifier1, Setting.ComboModifier2, Setting.ComboLetter, this);
            GHK.Register();
            this.Hide();
            var msg = string.Format("App is running in the background.{0}Your HotKey Combo is {1}", Environment.NewLine, Setting.GetShortcutLabel());
            niTray.ShowBalloonTip(3000, "PassCache Tray", msg , ToolTipIcon.Info);
        }

        private void HandleHotKeyPressEvent()
        {
            if (!Clipboard.ContainsText())
            {
                niTray.ShowBalloonTip(3000, "PassCache Tray", "Clipboard must contain text to use passcache.", ToolTipIcon.Error);
                return;
            }

            var data = Clipboard.GetText();
            var result = Worker.EncryptClipboard(data);
            
            Worker.PostToServer(result);
        
            Clipboard.SetText(result.FullUrl);
        
            niTray.ShowBalloonTip(3000, "PassCache Tray", "URL has been copied to your clipboard.", ToolTipIcon.Info);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
                HandleHotKeyPressEvent();
            base.WndProc(ref m);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Setting = new PassCacheTraySetting(Settings.Default);
            Worker = new Worker(Setting.PassCacheUrl);

            cbxModifier1.DataSource = new List<HotKeyModifiers>
            {
                HotKeyModifiers.CTRL,
                HotKeyModifiers.ALT,
                HotKeyModifiers.SHIFT
            };
            cbxModifier2.DataSource = new List<HotKeyModifiers>
            {
                HotKeyModifiers.CTRL,
                HotKeyModifiers.ALT,
                HotKeyModifiers.SHIFT
            };
            
            cbxModifier1.SelectedItem = Setting.ComboModifier1;
            cbxModifier2.SelectedItem = Setting.ComboModifier2;
            tbxLetter.Text = Setting.ComboLetter.ToString();
            tbxServerUri.Text = Setting.PassCacheUrl;
        }

        #region Validation Stuff

        private void cbxModifier2_Validating(object sender, CancelEventArgs e)
        {
            var cb = sender as ComboBox;

            if (cb.SelectedValue.Equals(cbxModifier1.SelectedValue))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbxModifier2, "You must select 2 seperate modifiers.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void cbxModifier1_Validating(object sender, CancelEventArgs e)
        {
            var cb = sender as ComboBox;

            if (cb.SelectedValue.Equals(cbxModifier2.SelectedValue))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbxModifier1, "You must select 2 seperate modifiers.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void tbxLetter_Validating(object sender, CancelEventArgs e)
        {
            var t = sender as TextBox;

            if (t == null) return;

            if (string.IsNullOrEmpty(t.Text) || t.Text.Length > 1 || !t.Text.IsLetter())
            {
                e.Cancel = true;
                errorProvider1.SetError(tbxLetter, "Shortcut Combo must have a Single Letter");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        #endregion 

        #region Menu Stuff
        
        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Are you sure you want to close PassCache Tray?", "Exit PassCache Tray",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            if (GHK != null)
            {
                GHK.Unregister();
            }

            niTray.Visible = false;
            Application.DoEvents();
            Environment.Exit(0);
            
        }

        #endregion

    }
}
