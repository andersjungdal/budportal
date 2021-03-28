using System;
using System.Threading.Tasks;

namespace BlazorBusinessLogic.Extensions
{
    public class MethodTranslation
    {
        public static async void AsyncTranslation(Func<Task> Method)
        {
            await Method();
        }
        public static async void AsyncTranslation<T>(Func<Task<T>> Method, Output<T> output)
        {
            output.Result = await Method();
        }
        
    }

    public class Output<T>
    {
        public T Result;
    }
}