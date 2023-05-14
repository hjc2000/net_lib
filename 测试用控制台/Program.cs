using NetworkInterfaceLib;

NetInterfaceInfoCollection netInterfaceInfoCollection = new NetInterfaceInfoCollection();
foreach (NetInterfaceInfo netInterfaceInfo in netInterfaceInfoCollection)
{
	Console.WriteLine(netInterfaceInfo.Description);
	foreach (string uip in netInterfaceInfo.UnicastIPAddressInformationArr)
	{
		Console.WriteLine(uip);
	}
	Console.WriteLine();
}