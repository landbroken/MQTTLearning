using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttServerTest
{
    /// <summary>
    /// 测试string和string
    /// </summary>
    public class EquipmentDataJson
    {
        public string str_test { get; set; }
        public string[] str_arr_test { get; set; }
        public int int_test { get; set; }
        public int[] int_arr_test { get; set; }
    }

    public class AllData
    {
        public EquipmentDataJson m_data = new EquipmentDataJson();
    }
}
