namespace RapidDevelopment_KupaKuper
{
    public class BaseMode
    {
        public string IP { get; set; } = "opc.tcp://192.168.30.88:4840";
        public string HeartbeatAddress { get; set; } = "ns=4;s=|var|Inovance-PLC.Application.GVL.Heart";
    }
}
