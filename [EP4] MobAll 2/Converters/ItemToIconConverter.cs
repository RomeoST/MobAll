using System;
using System.Globalization;
using System.Windows.Data;
using FieryLib.Models;
using _EP4__MobAll_2.WindowWPF;

namespace _EP4__MobAll_2.Converters
{
    public class ItemToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if ((value is ItemAllLod))
                return IconConverter.GetIconByItem(value as ItemAllLod);
            else if ((value is ItemInfo))
                return IconConverter.GetIconByItem(value as ItemInfo);
            else if ((value is DropAll.Items))
                return IconConverter.GetIconByItem(value as DropAll.Items);
            else return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
