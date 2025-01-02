using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Game2048.Infrastructure.Converters
{
    internal class ValueToColorConverter : IValueConverter
    {
        #region Brushes
        private static readonly SolidColorBrush tile2Brush = GetSolidColorBrush(192, 192, 192);
        private static readonly SolidColorBrush tile4Brush = GetSolidColorBrush(169, 169, 169);
        private static readonly SolidColorBrush tile8Brush = GetSolidColorBrush(100, 149, 237);
        private static readonly SolidColorBrush tile16Brush = GetSolidColorBrush(65, 105, 255);
        private static readonly SolidColorBrush tile32Brush = GetSolidColorBrush(138, 33, 255);
        private static readonly SolidColorBrush tile64Brush = GetSolidColorBrush(146, 0, 10);
        private static readonly SolidColorBrush tile128Brush = GetSolidColorBrush(170, 115, 221);
        private static readonly SolidColorBrush tile256Brush = GetSolidColorBrush(218, 165, 32);
        private static readonly SolidColorBrush tile512Brush = GetSolidColorBrush(255, 168, 18);
        private static readonly SolidColorBrush tile1024Brush = GetSolidColorBrush(255, 140, 0);
        private static readonly SolidColorBrush tile2048Brush = GetSolidColorBrush(255, 69, 0);
        private static readonly SolidColorBrush tile4096Brush = GetSolidColorBrush(0, 87, 237);
        private static readonly SolidColorBrush tile8192Brush = GetSolidColorBrush(255, 117, 24);
        private static readonly SolidColorBrush tile16384Brush = GetSolidColorBrush(255, 69, 0);
        private static readonly SolidColorBrush tile32768Brush = GetSolidColorBrush(201, 94, 22);
        private static readonly SolidColorBrush tile65536Brush = GetSolidColorBrush(207, 181, 59);
        private static readonly SolidColorBrush tileEmptyBrush = GetSolidColorBrush(217, 174, 125);
        #endregion

        private static readonly Dictionary<string, Brush> BrushDictionary = new()
        {
            {"2", tile2Brush},
            {"4", tile4Brush},
            {"8", tile8Brush},
            {"16", tile16Brush},
            {"32", tile32Brush},
            {"64", tile64Brush},
            {"128", tile128Brush},
            {"256", tile256Brush},
            {"512", tile512Brush},
            {"1024", tile1024Brush},
            {"2048", tile2048Brush},
            {"4096", tile4096Brush},
            {"8192", tile8192Brush},
            {"16384", tile16384Brush},
            {"32768", tile32768Brush},
            {"65536", tile65536Brush},
        };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (BrushDictionary.TryGetValue(value as string, out var brush))
                return brush;
            else
                return tileEmptyBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static SolidColorBrush GetSolidColorBrush(byte r = 0, byte g = 0 , byte b = 0, byte a = 255)
        {
            return new SolidColorBrush(GetColor(r, g, b, a));
        }

        private static System.Windows.Media.Color GetColor(byte r = 0, byte g = 0, byte b = 0, byte a = 255)
        {
            return new System.Windows.Media.Color { R = r, G = g, B = b, A = a }; 
        }
    }
}
