using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifxNet;

public class LightAids : MonoBehaviour
{

    [Range (0f, 65535f)]
    public float hue;

    [Range (0f, 65535f)]
    public float sat;

    [Range (0f, 65535f)]
    public float brightness;

    [Range (0f, 10f)]
    public float duration;

    private LifxClient client;

    // Start is called before the first frame update
void Start() {
    StartLivX();
}

    async void StartLivX()
    {
        client = await LifxClient.CreateAsync();
	    client.DeviceDiscovered += Client_DeviceDiscovered;
	    // client.DeviceLost += Client_DeviceLost;
	    client.StartDeviceDiscovery();
    }

    private async void Client_DeviceDiscovered(object sender, LifxClient.DeviceDiscoveryEventArgs e)
	{
		var bulb = e.Device as LightBulb;
		await client.SetDevicePowerStateAsync(bulb, true); //Turn bulb on
        
		// await client.SetColorAsync(bulb, red, 2700); //Set color to Red and 2700K Temperature			
        // LightBulb bulb,
		// 	UInt16 hue,
		// 	UInt16 saturation,
		// 	UInt16 brightness,
		// 	UInt16 kelvin,
		// 	TimeSpan transitionDuration

        await client.SetColorAsync(bulb,
			(ushort)hue,
			(ushort)sat,
			(ushort)brightness,
			2700,
			new System.TimeSpan(0,0, (int)duration)
        );
	}
}
