using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Models;

namespace TestProject.Core.services
{
   public class ChatService
    {
        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;

        public event EventHandler<ChatMessage> OnMessageReceived;

        public ChatService()
        {
            _connection = new HubConnection("http://10.10.3.221:58778/");
            _proxy = _connection.CreateHubProxy("TasksHub");
            _proxy.On("CallBack", (string name, string message) => OnMessageReceived(this, new ChatMessage
            {
                Name = name,
                Message = message
            }));
            OnMessageReceived += TestMethod;
        }

        private void TestMethod(object sender, ChatMessage e)
        {
            throw new NotImplementedException();
        }

        public async Task Connect()
        {

            var http = new Microsoft.AspNet.SignalR.Client.Http.DefaultHttpClient();
            //var transports = new List<IClientTransport>()
            //                                                        {
            //                                                            new WebSocketTransportLayer(http),
            //                                                            new ServerSentEventsTransport(http),
            //                                                            new LongPollingTransport(http)
            //                                                        };
            /// Preparando la conexion
            //await _connection.Start(new AutoTransport(http, transports));
            await _connection.Start();
        }

        public async Task Send(ChatMessage message, string roomName)
        {
            if (_connection.State == ConnectionState.Disconnected)
            {
                await Connect();
            }
            await _proxy.Invoke("SendMessage", message.Name, message.Message, roomName);
            
        }
        
        
        public async Task JoinGroup(string roomName)
        {
            if (_connection.State == ConnectionState.Disconnected)
            {
                await Connect();
            }
            await _proxy.Invoke("JoinGroup", roomName);
        }

    }
}
