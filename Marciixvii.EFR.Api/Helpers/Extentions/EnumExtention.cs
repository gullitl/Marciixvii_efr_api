using System;
using System.ComponentModel;
using System.Reflection;

namespace Marciixvii.EFR.App.Helpers.Extentions {
    public static class EnumExtention {
        public static string GetEnumDescription(this Enum value) {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute) fieldInfo?.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }

    }
}
