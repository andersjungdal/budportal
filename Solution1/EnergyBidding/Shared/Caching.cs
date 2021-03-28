using System;
using System.Threading.Tasks;
using ModelsInterfaces;

namespace EnergyBidding.Shared
{
    public class Caching<T> : ICaching<T>
    {
        private T value;
        public T Value
        {
            get
            {
                if (Created.Add(CachingTimeSpan) < DateTime.Now && LoadingFrom != null)
                {
                    LoadingFrom.Start();
                    value = LoadingFrom.Result;
                    Created = DateTime.Now;
                }
                return value;
            }
            set
            {
                this.value = value;
                Created = DateTime.Now;
            }
        }

        public DateTime Created { get; set; }
        public TimeSpan CachingTimeSpan { get; set; }
        public Task<T> LoadingFrom { get; set; }
    }
}