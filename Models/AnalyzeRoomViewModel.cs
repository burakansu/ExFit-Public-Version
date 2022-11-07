﻿using DatabaseLayer;
using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class AnalyzeRoomViewModel
    {
        public ObjUser User { get; set; }
        public int[] MemberMeazurementsArray { get; set; }
        public int[] CostArray { get; set; }
        public int[] IncomeArray { get; set; }
        public List<ObjTask> Tasks { get; set; }
        public List<ObjCost> Costs { get; set; }
        public ObjCost Cost { get; set; }
        public List<ObjIncome> Incomes { get; set; }
        public int Income { get; set; }

        // ViewModel in içine gömülü sanal property ler.

        public int Profit
        {
            get
            {
                SQL sQL = new SQL();
                return this.Income - sQL.Value<int>("SELECT SUM(Rent,Electric,Water,Staff_Salaries,Other) FROM TBL_Cost");
            }
        }
    }
}
