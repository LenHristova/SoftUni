using System.Collections.Generic;

namespace P04.Recharge
{
  public  class RechargeStation
    {
        public RechargeStation(IEnumerable<IRechargeable> rechargeables)
        {
            foreach (var rechargeable in rechargeables)
            {
                rechargeable.Recharge();
            }
        }
    }
}
