using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PassCacheTray.Models;

namespace PassCacheTray.Util
{
    public class GlobalHotkey
    {
        private int _modifier;
        private int _key;
        private IntPtr _hWnd;
        private int _id;

        public GlobalHotkey(HotKeyModifiers comboKey1, HotKeyModifiers comboKey2, Keys key, Form form)
        {
            _modifier = (int)comboKey1 + (int)comboKey2; ;
            _key = (int)key;
            _hWnd = form.Handle;
            _id = GetHashCode();
        }

        public bool Register()
        {
            return RegisterHotKey(_hWnd, _id, _modifier, _key);
        }

        public bool Unregister()
        {
            return UnregisterHotKey(_hWnd, _id);
        }

        public override int GetHashCode()
        {
            return _modifier ^ _key ^ _hWnd.ToInt32();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        
    }
}
