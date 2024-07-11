using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsAlwaysOn
{ 
    internal static class Program
    {
        [Flags]
        private enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        [STAThread]
        static void Main()
        {
            var icon = new NotifyIcon();
            icon.Icon = Properties.Resources.WindowsAlwaysOn;
            icon.Text = "WindowsAlwaysOn";
            
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem disableSleepOption = new ToolStripMenuItem("Disable Sleep");
            ToolStripMenuItem enableSleepOption = new ToolStripMenuItem("Enable Sleep");
            ToolStripMenuItem exitOption = new ToolStripMenuItem("Exit");
            
            menu.Items.Add(disableSleepOption);
            menu.Items.Add(enableSleepOption);
            menu.Items.Add(exitOption);
            exitOption.Click += exit;
            enableSleepOption.Click += enableSleep;
            disableSleepOption.Click += disableSleep;
            
            icon.ContextMenuStrip = menu;
            icon.Visible = true;
            
            Application.Run();
        }

        public static void exit(object sender, EventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            Application.Exit();
        }
        public static void enableSleep(object sender, EventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
        public static void disableSleep(object sender, EventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
        }
    }
}