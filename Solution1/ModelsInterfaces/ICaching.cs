using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelsInterfaces
{
    public interface ICaching<T>
    {
        T Value { get;}
        DateTime Created { get; set; }
        TimeSpan CachingTimeSpan { get; set; }
        Task<T> LoadingFrom { get; set; }
    }
}