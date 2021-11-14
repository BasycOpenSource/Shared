using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basyc.Shared.Helpers;
public static class GenericsHelper
{
    public static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        foreach (var it in interfaceTypes)
        {
            if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            return true;

        Type baseType = givenType.BaseType;
        if (baseType == null)
            return false;

        return IsAssignableToGenericType(baseType, genericType);
    }

    /// <summary>
    /// Get generic argument from base class
    /// </summary>
    /// <param name="type"></param>
    /// <param name="baseType">parent type that should contain generic parameters</param>
    /// <returns></returns>

    public static Type[] GetTypeArgumentsFromParent(Type type, Type baseType)
    {
        if (baseType.IsInterface)
        {
            var baseInterface = type.GetInterface(baseType.Name);
            return baseInterface.GetGenericArguments();
        }

        while (type.BaseType != null)
        {
            type = type.BaseType;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == baseType)
            {
                return type.GetGenericArguments();
            }
        }
        if (type != typeof(object))
        {
            throw new InvalidOperationException("Class does not have specified base class");
        }
        else
        {
            return new Type[0];
        }
    }
}