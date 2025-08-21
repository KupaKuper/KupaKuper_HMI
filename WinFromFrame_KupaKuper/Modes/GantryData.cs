using KupaKuper_HMI_Config.DeviceConfig.BaseType;

namespace WinFromFrame_KupaKuper.Modes
{
    public class GantryData
    {
        public readonly Axis Axis_X;
        public readonly Axis Axis_Y;
        public readonly Axis? Axis_Z;
        /// <summary>
        /// 龙门架
        /// </summary>
        /// <param name="axis_x"></param>
        /// <param name="axis_y"></param>
        /// <param name="axis_z"></param>
        public GantryData(Axis axis_x, Axis axis_y, Axis? axis_z)
        {
            Axis_X = axis_x;
            Axis_Y = axis_y;
            Axis_Z = axis_z;
        }
        /// <summary>
        /// 龙门点位
        /// </summary>
        public Dictionary<string, string> GantryPositions { get; set; } = new();
        /// <summary>
        /// 获取龙门点位对应的三个轴点位
        /// </summary>
        /// <param name="GantryPositionsKey"></param>
        /// <param name="GantryPosition"></param>
        /// <returns></returns>
        public bool GetGantryPosition(string GantryPositionsKey, out GantryPosition? GantryPosition)
        {
            if (GantryPositions.TryGetValue(GantryPositionsKey, out var value))
            {
                var positions = value.Split(',');
                int.TryParse(positions[0], out int x);
                int.TryParse(positions[1], out int y);
                int? z = int.TryParse(positions[2], out int zValue) ? zValue : null;
                try
                {
                    AxisPosition axisPosition_x = (AxisPosition)Axis_X.ListPosition.Find(p => p.PositionNo == x)!;
                    AxisPosition axisPosition_y = (AxisPosition)Axis_Y.ListPosition.Find(p => p.PositionNo == y)!;
                    AxisPosition? axisPosition_z = Axis_Z == null ? null : (AxisPosition)Axis_Z.ListPosition.Find(p => p.PositionNo == z)!;
                    GantryPosition = new(axisPosition_x, axisPosition_y, axisPosition_z);
                }
                catch 
                {
                    GantryPosition = null;
                    return false;
                }
                return true;
            }
            else
            {
                GantryPosition = null;
                return false;
            }
        }
    }
    public class GantryPosition
    {
        public AxisPosition AxisPosition_x;
        public AxisPosition AxisPosition_y;
        public AxisPosition? AxisPosition_z;
        public GantryPosition(AxisPosition axisPosition_x, AxisPosition axisPosition_y, AxisPosition? axisPosition_z)
        {
            AxisPosition_x = axisPosition_x;
            AxisPosition_y = axisPosition_y;
            AxisPosition_z = axisPosition_z;
        }
    }
}
