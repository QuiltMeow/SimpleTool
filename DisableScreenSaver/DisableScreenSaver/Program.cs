using DisableScreenSaver.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DisableScreenSaver
{
    public static class Program
    {
        private const string title = "防止電腦鎖定";
        private static readonly NotifyIcon icon = new NotifyIcon();
        private static readonly ContextMenuStrip menu = new ContextMenuStrip();
        private static readonly ToolStripMenuItem tsmiPreventScreenSaver = new ToolStripMenuItem();
        private static readonly ToolStripMenuItem tsmiExit = new ToolStripMenuItem();

        private static void initialize()
        {
            icon.ContextMenuStrip = menu;
            icon.Icon = (Icon)Resources.ResourceManager.GetObject("Icon");
            icon.Text = title;
            icon.Visible = true;

            menu.Items.AddRange(new ToolStripItem[]
            {
                tsmiPreventScreenSaver,
                tsmiExit
            });
            menu.Size = new Size(147, 26);

            tsmiPreventScreenSaver.Checked = true;
            tsmiPreventScreenSaver.CheckOnClick = true;
            tsmiPreventScreenSaver.CheckState = CheckState.Checked;
            tsmiPreventScreenSaver.Size = new Size(180, 22);
            tsmiPreventScreenSaver.Text = title;
            tsmiPreventScreenSaver.Click += new EventHandler((sender, eventArgument) => Util.preventScreenSaver(tsmiPreventScreenSaver.Checked));

            tsmiExit.Size = new Size(180, 22);
            tsmiExit.Text = "離開";
            tsmiExit.Click += new EventHandler((sender, eventArgument) =>
            {
                icon.Visible = false;
                Environment.Exit(Environment.ExitCode);
            });
        }

        [STAThread]
        public static void Main()
        {
            initialize();
            Util.preventScreenSaver(true);
            Application.Run();
        }
    }
}