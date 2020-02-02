using System;

namespace MyWalletApp.WebApi.Mappers
{
    public static class EnumMapper
    {
         public static T Map<F, T> (F fromEnum)
         where F: struct, Enum
         where T: struct, Enum
        {
            var parsed = Enum.TryParse(fromEnum.ToString(), out T toEnum);

            if (!parsed || !Enum.IsDefined(typeof(T), toEnum))
            {
                throw new NotSupportedException($"Cannot map {nameof(toEnum)} with value {fromEnum}");
            }

            return toEnum;
        }
    }
}