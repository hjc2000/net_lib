using System.Net;
using System.Numerics;

namespace NetLib.IP;
public static class IPConverter
{
	public static BigInteger ToBigInteger(this IPAddress ip)
	{
		return new BigInteger(ip.GetAddressBytes().Reverse().ToArray());
	}

	public static IPAddress ToIPAddress(this BigInteger ipBigInt)
	{
		return new IPAddress(ipBigInt.ToByteArray().Reverse().ToArray());
	}
}
