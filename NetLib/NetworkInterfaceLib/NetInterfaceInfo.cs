namespace NetLib.NetworkInterfaceLib
{
	/// <summary>
	/// 储存单个网卡的信息。
	/// 1. 描述
	/// 2. 非广播地址
	/// 3. 广播地址
	/// </summary>
	public class NetInterfaceInfo
	{
		public NetInterfaceInfo(string description, string[] unicastIPAddressInformationArr,
			string[] multicastIPAddressInformationArr)
		{
			Description = description;
			UnicastIPAddressInformationArr = unicastIPAddressInformationArr;
			MulticastIPAddressInformationArr = multicastIPAddressInformationArr;
		}
		public string Description { get; }
		public string[] UnicastIPAddressInformationArr { get; }
		public string[] MulticastIPAddressInformationArr { get; }
	}
}
