using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace P01_Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string classToInvestigate, params string[] fieldsToInvestigate)
        {
            var classType = Type.GetType(classToInvestigate);
            var classFields = classType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

            var sb = new StringBuilder();

            var classInstance = Activator.CreateInstance(classType);

            sb.AppendLine($"Class under investigation: {classToInvestigate}");

            foreach (var classField in classFields.Where(f => fieldsToInvestigate.Contains(f.Name)))
            {
                sb.AppendLine($"{classField.Name} = {classField.GetValue(classInstance)}");
            }

            return sb.ToString().Trim();
        }

        public string AnalyzeAcessModifiers(string classToInvestigate)
        {
            var sb = new StringBuilder();

            var classType = Type.GetType(classToInvestigate);

            var classFields = classType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            foreach (var field in classFields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }


            var classNonPubicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var method in classNonPubicMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }

            var classPubicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in classPubicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }
            return sb.ToString().Trim();
        }

        public string RevealPrivateMethods(string classToInvestigate)
        {
            var classType = Type.GetType(classToInvestigate);
            var classNonPubicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            var sb = new StringBuilder();

            sb.AppendLine($"All Private Methods of Class: {classToInvestigate}");
            sb.AppendLine($"Base Class: {classType.BaseType.Name}");
            foreach (var method in classNonPubicMethods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString().Trim();
        }

        public string CollectGettersAndSetters(string classToInvestigate)
        {
            var sb = new StringBuilder();

            var classType = Type.GetType(classToInvestigate);

            var properties = classType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in properties.Where(p => p.GetMethod != null))
            {
                sb.AppendLine($"{property.GetMethod.Name} will return {property.GetMethod.ReturnType}");
            }


            foreach (var property in properties.Where(p => p.SetMethod != null))
            {
                sb.AppendLine($"{property.SetMethod.Name} will set field of {property.SetMethod.GetParameters().First().ParameterType}");
            }
            //MethodInfo[] classMethods =
            //    classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            //foreach (MethodInfo method in classMethods.Where(m => m.Name.StartsWith("get")))
            //{
            //    sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            //}

            //foreach (MethodInfo method in classMethods.Where(m => m.Name.StartsWith("set")))
            //{
            //    sb.AppendLine(
            //        $"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            //}


            return sb.ToString().TrimEnd();
        }
    }
}