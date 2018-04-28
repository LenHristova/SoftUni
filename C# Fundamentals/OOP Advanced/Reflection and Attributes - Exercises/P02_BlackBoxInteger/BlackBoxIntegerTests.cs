using System.Reflection;

namespace P02_BlackBoxInteger
{
    using System;

    public class BlackBoxIntegerTests
    {
        private const BindingFlags BINDING_ATTR = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static void Main()
        {
            var blackBox = Activator.CreateInstance(typeof(BlackBoxInteger), true);


            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var args = input.Split('_');
                var methodName = args[0];
                var methodParameter = int.Parse(args[1]);

                var method = blackBox.GetType().GetMethod(methodName, BINDING_ATTR, null, new Type[]{typeof(int)}, null );
                //var method = blackBox.GetType().GetMethod(methodName, BINDING_ATTR);

                if (method != null)
                {
                    method.Invoke(blackBox, new object[] { methodParameter });
                    var innerValue = blackBox.GetType().GetField("innerValue", BINDING_ATTR);
                    //var innerValueType = innerValue.FieldType;

                    var valueObj = innerValue.GetValue(blackBox);
                    //var value = Convert.ChangeType(valueObj, innerValueType);

                    Console.WriteLine(valueObj.ToString());
                    //Console.WriteLine(value);
                }
            }
        }
    }
}
