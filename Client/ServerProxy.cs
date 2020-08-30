using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ServerProxy : DuplexClientBase<IServerService>
    {
        public ServerProxy(IClientService callbackInstance) : base(callbackInstance)
        { }
    }
}
