namespace NetLib.NetworkInterfaceLib
{
	/// <summary>
	/// 储存单个网卡的信息。
	/// </summary>
	public class NetInterfaceInfo
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="description">网卡的描述信息，一般可以认为就是网卡的名称</param>
		/// <param name="unicastIPAddressInformationArr">网卡的非广播地址</param>
		/// <param name="multicastIPAddressInformationArr">网卡的广播地址</param>
		public NetInterfaceInfo(string description, string[] unicastIPAddressInformationArr,
			string[] multicastIPAddressInformationArr)
		{
			Description = description;
			UnicastIPAddressInformationArr = unicastIPAddressInformationArr;
			MulticastIPAddressInformationArr = multicastIPAddressInformationArr;
		}

		/// <summary>
		/// 网卡的描述信息，一般可以认为就是网卡的名称
		/// </summary>
		public string Description { get; }

		/// <summary>
		/// 网卡的非广播地址
		/// </summary>
		public string[] UnicastIPAddressInformationArr { get; }

		/// <summary>
		/// 网卡的广播地址
		/// </summary>
		public string[] MulticastIPAddressInformationArr { get; }
	}
}
