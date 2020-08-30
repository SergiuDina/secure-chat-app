using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [DataContract]
    public class EncMessage
    {
        [DataMember]
        public ConnectedClient Sender { get; set; }

        [DataMember]
        public ConnectedClient Recipient { get; set; }

        [DataMember]
        public bool UsedForDHExchange { get; set; }

        [DataMember]
        public byte[] Payload { get; set; }
    }
}
