using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WPF_TEST.NativeMethods;
using System.Windows.Interop;
using System.Windows;

namespace WPF_TEST.Helpers
{
    internal class BlurHelper
    {
        private uint blurOpacity;
        public double BlurOpacity
        {
            get { return blurOpacity; }
            set { blurOpacity = (uint)value; EnableBlur(); }
        }

        private readonly uint blurBackgroundColor = 0x990000;

        private Window Window { get; set; }
        private AccentState AccentState { get; set; }

        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(Window);

            var accent = new AccentPolicy
            {
                AccentState = this.AccentState,
                GradientColor = (blurOpacity << 24) | (blurBackgroundColor & 0xFFFFFF) /*(White mask 0xFFFFFF)*/
            };

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }


        internal BlurHelper(Window window, AccentState accentState)
        {
            this.Window = window;
            this.AccentState = accentState;
            EnableBlur();
        }
    }
}
