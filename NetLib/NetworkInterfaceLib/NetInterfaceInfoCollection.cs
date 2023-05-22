using System.Collections;
using System.Net.NetworkInformation;

namespace NetLib.NetworkInterfaceLib;

/// <summary>
/// 储存多个网卡的信息。其中每个网卡的信息用 NetInterfaceInfo 类包装
/// </summary>
public class NetInterfaceInfoCollection : IEnumerable
{
	public NetInterfaceInfoCollection()
	{
		NetworkInterface[] netInterfaceArr = NetworkInterface.GetAllNetworkInterfaces();
		_infoArr = new NetInterfaceInfo[netInterfaceArr.Length];
		for (int i = 0; i < netInterfaceArr.Length; i++)
		{
			string description = netInterfaceArr[i].Description;
			string[] uips = GetUnicastIPAddressInformationArr(netInterfaceArr[i]);
			string[] mips = GetMulticastIPAddressInformationArr(netInterfaceArr[i]);
			_infoArr[i] = new NetInterfaceInfo(description, uips, mips);
		}
	}

	/// <summary>
	/// 获取非广播地址
	/// </summary>
	/// <param name="networkInterface"></param>
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
	/// 获取广播地址
	/// </summary>
	/// <param name="networkInterface"></param>
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

	public IEnumerator GetEnumerator()
	{
		return _infoArr.GetEnumerator();
	}

	/// <summary>
	/// 储存每个网卡信息的数组
	/// </summary>
	private readonly NetInterfaceInfo[] _infoArr;

	public NetInterfaceInfo this[int index]
	{
		get => _infoArr[index];
		set => _infoArr[index] = value;
	}

	public int Count => _infoArr.Length;
}
