
using ENTOBEL_AURAVINA_API.MQTTClients;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;

namespace ENTOBEL_AURAVINA_API.Hubs
{
    public class ScadaHost : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ScadaHost(ManagedMqttClient mqttClient, IHubContext<NotificationHub> hubContext)
        {
            _mqttClient = mqttClient;
            _hubContext = hubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectToMqttBrokerAsync();
        }
        private async Task ConnectToMqttBrokerAsync()
        {
            await _mqttClient.ConnectAsync();

            await _mqttClient.Subscribe("TestModbusTCPIP/+");

            _mqttClient.MessageReceived += OnMqttClientMessageReceived;
        }

        private async Task OnMqttClientMessageReceived(MqttMessage arg)
        {
            var topic = arg.Topic;
            var payloadMessage = arg.Payload;

            if (topic is null || payloadMessage is null)
            {
                return;
            }

            var json = System.Text.Json.JsonSerializer.Serialize(payloadMessage);
            //Console.WriteLine(payloadMessage);
            await _hubContext.Clients.All.SendAsync("GetAll", json);
            
        }
    }
}
