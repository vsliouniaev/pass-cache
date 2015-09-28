using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PassCacheTray.Properties;

namespace PassCacheTray.Models
{
    internal class PassCacheTraySetting
    {
        public HotKeyModifiers ComboModifier1 { get; set; }
        public HotKeyModifiers ComboModifier2 { get; set; }
        public Keys ComboLetter { get; set; }
        public string PassCacheUrl { get; set; }

        public PassCacheTraySetting(Settings s)
        {
            ComboModifier1 = (HotKeyModifiers)Enum.Parse(typeof(HotKeyModifiers), s.ComboKey1);
            ComboModifier2 = (HotKeyModifiers)Enum.Parse(typeof(HotKeyModifiers), s.ComboKey2);
            ComboLetter = s.ComboLetter;
            PassCacheUrl = s.PassCacheUrl;
        }

        public void SaveToSettings(Settings s)
        {
            s.ComboKey1 = ComboModifier1.ToString();
            s.ComboKey2= ComboModifier2.ToString();
            s.ComboLetter = ComboLetter;
            s.PassCacheUrl = PassCacheUrl;

            s.Save();
        }

        public string GetShortcutLabel()
        {
            return string.Format("{0} + {1} + {2}", ComboModifier1, ComboModifier2, ComboLetter);
        }
    }
}
