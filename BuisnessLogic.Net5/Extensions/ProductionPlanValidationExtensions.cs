using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyBidding.Shared.Documents.ProductionPlanXml;

namespace BlazorBuisnessLogic.Net5.Extensions
{
    public static class ProductionPlanValidationExtensions
    {
        public static bool ValidateProductionPlanMinMax(this OperationalScheduleDocument productionPlan)
        {
            OperationalScheduleTimeSeries Value = null;
            OperationalScheduleTimeSeries min = null;
            OperationalScheduleTimeSeries max = null;
            List<Task<bool>> comparisons = new List<Task<bool>>(productionPlan.OperationalScheduleTimeSeries.Count/3);
            for (int i = 0; i < productionPlan.OperationalScheduleTimeSeries.Count; i ++)
            {
                if (productionPlan.OperationalScheduleTimeSeries[i].BusinessType.V.Equals("MAX", StringComparison.OrdinalIgnoreCase))
                {
                    max = productionPlan.OperationalScheduleTimeSeries[i];
                }
                else if (productionPlan.OperationalScheduleTimeSeries[i].BusinessType.V.Equals("MIN", StringComparison.OrdinalIgnoreCase))
                {
                    min = productionPlan.OperationalScheduleTimeSeries[i];
                }
                else
                {
                    Value = productionPlan.OperationalScheduleTimeSeries[i];
                }
                if ((Value?.UnitIdentification?.V == min?.UnitIdentification?.V &&
                    Value?.UnitIdentification?.V == max?.UnitIdentification?.V && Value?.UnitIdentification?.V != null)||(
                    Value?.UnitTypeIdentification?.V == min?.UnitTypeIdentification?.V && 
                    Value?.UnitTypeIdentification?.V == max?.UnitTypeIdentification?.V && Value?.UnitTypeIdentification?.V != null))
                {
                    comparisons.Add(ValidateTimesSeries(Value,min,max));
                }
            }
            while(comparisons.Count>0)
            {
                Task<Task<bool>> FinishTask = Task.WhenAny(comparisons);
                var test = FinishTask.Result.Result;
                if (!test)
                {
                    return false;
                }
                comparisons.Remove(FinishTask.Result);
            }
            return true;
        }

        public static async Task<bool> ValidateTimesSeries(OperationalScheduleTimeSeries Value, OperationalScheduleTimeSeries min,
            OperationalScheduleTimeSeries max)
        {
            for (int i = 0; i < Value.Period.Interval.Count; i++)
            {
                if (Value.Period.Interval[i].Quantity.V < min.Period.Interval[i].Quantity.V ||
                    Value.Period.Interval[i].Quantity.V > max.Period.Interval[i].Quantity.V)
                {
                    return false;
                }
            }
            return true;
        }
    }
}