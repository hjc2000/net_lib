using System.Collections;
using System.Net.NetworkInformation;

namespace NetLib.NetworkInterfaceLib;

/// <summary>
/// 储存多个网卡的信息。其中每个网卡的信息用 NetInterfaceInfo 类包装
/// </summary>
public class NetInterfaceInfoCollection : IEnumerable
{
	#region 生命周期
	/// <summary>
	/// 构造函数，获取系统中的所有网卡的信息，然后储存到本类中
	/// </summary>
	public NetInterfaceInfoCollection()
	{
		// 获取系统中的所有网卡信息
		NetworkInterface[] netInterfaceArr = NetworkInterface.GetAllNetworkInterfaces();
		// 创建 NetInterfaceInfo 类的数组用来储存网卡信息
		_networkInterfaceInfoArr = new NetInterfaceInfo[netInterfaceArr.Length];
		// 遍历每一个网卡，将其部分信息复制到 NetInterfaceInfo 对象中，
		// 一般不用用到全部信息
		// 然后将 NetInterfaceInfo 对象放到数组中
		for (int i = 0; i < netInterfaceArr.Length; i++)
		{
			string description = netInterfaceArr[i].Description;
			string[] uips = GetUnicastIPAddressInformationArr(netInterfaceArr[i]);
			string[] mips = GetMulticastIPAddressInformationArr(netInterfaceArr[i]);
			_networkInterfaceInfoArr[i] = new NetInterfaceInfo(description, uips, mips);
		}
	}
	#endregion

	#region 私有数据处理方法
	/// <summary>
	/// 获取一个数组，里面储存着指定网卡的所有非广播 IP 地址
	/// </summary>
	/// <param name="networkInterface">指定的网卡</param>
	/// <returns></returns>
	private string[] GetUnicastIPAddressInformationArr(NetworkInterface networkInterface)
	{
		UnicastIPAddressInformationCollection unicastIPAddresseCollection = networkInterface.GetIPProperties().UnicastAddresses;
		// 要返回的结果
		string[] result = new string[unicastIPAddresseCollection.Count];
		for (int i = 0; i < unicastIPAddresseCollection.Count; i++)
		{
			UnicastIPAddressInformation unicastIPAddress = unicastIPAddresseCollection[i];
			result[i] = unicastIPAddress.Address.ToString();
		}

		return result;
	}

	/// <summary>
	/// 获取一个数组，里面储存着指定网卡的所有广播 IP 地址
	/// </summary>
	/// <param name="networkInterface">指定的网卡</param>
	/// <returns></returns>
	private string[] GetMulticastIPAddressInformationArr(NetworkInterface networkInterface)
	{
		MulticastIPAddressInformationCollection multicastAddressCollection = networkInterface.GetIPProperties().MulticastAddresses;
		string[] result = new string[multicastAddressCollection.Count];
		for (int i = 0; i < multicastAddressCollection.Count; i++)
		{
			MulticastIPAddressInformation multicastAddress = multicastAddressCollection[i];
			result[i] = multicastAddress.Address.ToString();
		}

		return result;
	}
	#endregion

	/// <summary>
	/// 提供迭代器
	/// </summary>
	/// <returns></returns>
	public IEnumerator GetEnumerator()
	{
		return _networkInterfaceInfoArr.GetEnumerator();
	}

	/// <summary>
	/// 储存每个网卡信息的数组
	/// </summary>
	private readonly NetInterfaceInfo[] _networkInterfaceInfoArr;

	/// <summary>
	/// 索引器，获取指定索引的网卡信息
	/// </summary>
	/// <param name="index">索引</param>
	/// <returns>储存着网卡信息的 NetInterfaceInfo 对象</returns>
	public NetInterfaceInfo this[int index]
	{
		get => _networkInterfaceInfoArr[index];
		set => _networkInterfaceInfoArr[index] = value;
	}

	/// <summary>
	/// 网卡的数量
	/// </summary>
	public int Count => _networkInterfaceInfoArr.Length;
}
