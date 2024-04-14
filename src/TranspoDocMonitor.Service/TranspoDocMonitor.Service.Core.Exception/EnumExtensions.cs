namespace TranspoDocMonitor.Service.Core.Exception
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            if (name == null)
                throw new InvalidOperationException($"Not found attribute with name {typeof(TAttribute).Name}");

            return enumType.GetField(name)!.GetCustomAttributes(false).OfType<TAttribute>().Single();
        }
    }
}
