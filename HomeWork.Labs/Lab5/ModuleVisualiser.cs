namespace Epam.HomeWork.Lab5
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class ModuleVisualiser
    {
        public static IEnumerable<string> GetInfo(Module module)
        {
            if (module is null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            List<string> info = new List<string>
            {
                $"Assembly: {module.Assembly}",
                $"Module: {module.Name}"
            };

            foreach (var type in module.GetTypes())
            {
                info.Add($"=> Type: {type.FullName}");

                foreach (var member in type.GetMembers())
                {
                    AddInfoAbourMembers(info, member);
                }
            }

            return info;
        }

        private static void AddInfoAbourMembers(List<string> info, MemberInfo member)
        {
            info.Add($"\tMember type: {member.MemberType}, member name: {member.Name}");

            if (member.MemberType == MemberTypes.Method)
            {
                var methdInfo = member as MethodInfo;

                foreach(var param in methdInfo.GetParameters())
                {
                    info.Add($"\t - Parameter: {param.ParameterType.Name}, {param.Name}");
                }
            }
        }
    }
}
