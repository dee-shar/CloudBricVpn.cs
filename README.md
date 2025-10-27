# CloudBricVpn.cs
Mobile-API for [CloudBric VPN](https://www.cloudbric.com/cloudbric-vpn/) an application that offers seamless, uninterrupted connectivity across a vast network of free servers located worldwide, with no limitations on speed or duration

## Example
```cs
using System;
using CloudBricVpnApi;

namespace Application
{
    internal class Program
    {
        static async Task Main()
        {
            var api = new CloudBricVpn();
            await api.Login("example@gmail.com", "password");
            string servers = await api.GetServers();
            Console.WriteLine(servers);
        }
    }
}
```
